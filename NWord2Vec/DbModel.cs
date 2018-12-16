using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using DBModelConnector;
using simple_text_mining_library;

namespace NWord2Vec
{
    public class DbModel
    {
        private DbModelConnector connector;

        public DbModel()
        {
            connector = DbModelConnector.GetConnector();
        }
        public void LoadModelToDb(string filePath)
        {
            using (var fileStream = OpenStream(filePath))
            {
                var ext = Path.GetExtension(filePath);
                if (ext == ".gz")
                {
                    ext = Path.GetExtension(Path.GetFileNameWithoutExtension(filePath));
                }

                var reader = GetReader(fileStream, ext.ToLower());
                reader.LoadToDb();
            }
        }

        public WordVector ReadWordVector(string word)
        {
            float[] tempWordVector = connector.GetVector(word);
            return new WordVector(word, tempWordVector);
        }
        public double GetWordDistance(string word1, string word2)
        {
            return connector.GetDistanceProcedure(word1, word2);
        }

        public List<WordVector> Nearest()
        {
            return new List<WordVector>();
        }
        public List<WordVector> CreateWordVectorList(string filePath)
        {
            List<WordVector> resultList = new List<WordVector>();
            var inputText = File.ReadAllText(filePath);
            WordVector tempWordVector; 
            var texCleaner = new TextConvertor();
            texCleaner.textMiningLanguage = simple_text_mining_library.Classes.TextMiningLanguage.English;
            inputText = texCleaner.RemoveSpecialCharacters(inputText, true);
            inputText = texCleaner.RemoveStopWordsFromText(inputText);
            var listText = texCleaner.N1GramAnalysis(inputText);
            foreach (string item in listText){
                tempWordVector = ReadWordVector(item);
                if(tempWordVector.Vector.Count()>0)
                resultList.Add(tempWordVector);
            }
            return resultList;

        }
        public List<WordVector> CreateWordVectorList(List<string> _words)
        {
            List<WordVector> resultList = new List<WordVector>();
            WordVector tempWordVector;

            var listText = _words;
            foreach (string item in listText)
            {
                tempWordVector = ReadWordVector(item);
                if (tempWordVector.Vector.Count() > 0)
                    resultList.Add(tempWordVector);
            }
            return resultList;

        }

        Stream OpenStream(string filePath)
        {
            var fileStream = File.OpenRead(filePath);
            if (Path.GetExtension(filePath).ToLower() == ".gz") return new GZipStream(fileStream, CompressionMode.Decompress);
            return fileStream;
        }

        IModelReader GetReader(Stream stream, string fileExtension)
        {
            switch (fileExtension)
            {
                case ".txt":
                    return new TextModelReader(stream);
                case ".bin":
                    return new BinaryModelReader(stream);
                default:
                    var error = new InvalidOperationException("Unrecognized file type");
                    error.Data.Add("extension", fileExtension);
                    throw error;
            }
        }
    }
}
