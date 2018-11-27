using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWord2Vec;
using DBModelConnector;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Connect Model to Db ");
            var model = new DbModel();
            
            var startTime = DateTime.Now;
            Load();
            while (false) {
            Console.Write("Enter first word: ");
            var word1 = Console.ReadLine();
            //var wordVector = model.ReadWordVector(word1);
            //Console.WriteLine("Word: {0}  FirstVector: {1}", wordVector.Word, wordVector.Vector.First());

            Console.Write("Enter second word: ");
            var word2 = Console.ReadLine();
            Console.WriteLine("Distance {0} ---> {1}: {2} ",word1,word2, model.GetWordDistance(word1,word2).ToString());
            }
            while (false)
            {
               
                string path = @"E:\Student\5CourseMaster\Diploma2018\MyDiplomaProject\SampleBooks\My sample text.txt"; //Console.ReadLine();
                Console.Write("Enter path to txt file: {0}", path);
                var wordVectorTextList = model.CreateWordVectorList(path);
                var textModel = new RealModel(wordVectorTextList.Count, wordVectorTextList.First().Vector.Length, wordVectorTextList);
                Console.Write("Enter main theme: ");
                string word = Console.ReadLine();
                var wordVector = model.ReadWordVector(word);
                var distanceList = textModel.GetWordDistances(wordVector);
                Console.WriteLine("Distance list computed!");
                var avarageDistance = distanceList.AvarageDistance() ;
                Console.WriteLine("Central Distance with {0}: {1}",word, avarageDistance);
                Console.ReadLine();
            }
            while (false)
            {
                Console.WriteLine("Enter word1: ");
                var word1 = Console.ReadLine();
                if (word1 == "//exit") break;
                Console.WriteLine("Enter word1: ");
                var word2 = Console.ReadLine();
                if (word2 == "//exit") break;

               // Console.WriteLine("Distance: {0}", model.Distance(word1, word2));
                Console.ReadLine();
                Console.WriteLine("Neares word 2: ");
                try
                {
                    //List<WordDistance> nearestWords = model.Nearest(word1).ToList();
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            void Load()
            {
                //var realModel = RealModel.Load(@"model.bin");
                //model.LoadModelToDb(@"model.bin"); //E:\Student\5CourseMaster\Diploma2018\Example program\Models\cc.en.300.bin
                model.LoadModelToDb(@"E:\Student\5CourseMaster\Diploma2018\Example program\Models\cc.en.300.bin");
                var finishTime = DateTime.Now;
                Console.WriteLine("Time: {0} ", finishTime - startTime);
                Console.ReadLine();
            }


        }
    }
}
