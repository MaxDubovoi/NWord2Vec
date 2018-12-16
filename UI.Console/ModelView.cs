using Clusteriser;
using Clusteriser.DTO;
using NWord2Vec;
using SearchServiceStub;
using simple_text_mining_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModel;

namespace UI.Console
{
    public class ModelView : IModelView
    {
        private IView _view;
        private DbModel _word2VecModelDB = new DbModel();
        private ModelView(IView view)
        {
            _view = view;
        }
        public static IModelView BindView(IView view)
        {
            return new ModelView(view);
        }

        public void SendQuery(string query)
        {
            var n1GrammQuery = TextToWordVector(query);
            var text = TextSearcher.Search(query);
            var n1GrammText = TextToWordVector(text);
            var vectorizedText = _word2VecModelDB.CreateWordVectorList(n1GrammText);
            var vectorizedQuery = _word2VecModelDB.CreateWordVectorList(n1GrammQuery);
            var score = GetAccordance(vectorizedText, vectorizedQuery);
            _view.ShowTextWithScore(text, score);


        }

        private List<string> TextToWordVector(string text)
        {
            string proccesedText;
            TextConvertor textConvertor = new TextConvertor();
            textConvertor.textMiningLanguage = simple_text_mining_library.Classes.TextMiningLanguage.English;
            proccesedText = textConvertor.RemoveStopWordsFromText(text);
            proccesedText = textConvertor.RemoveSpecialCharacters(proccesedText, true);
            return textConvertor.N1GramAnalysis(proccesedText);
        }
        private double GetAccordance(List<WordVector> _textVectors, List<WordVector> queryVectors)
        {
            var bufferTextList = new List<float[]>();
            var bufferQueryList = new List<float[]>();
            foreach (WordVector item in _textVectors)
            {
                bufferTextList.Add(item.Vector);
            }
            foreach (WordVector item in queryVectors)
            {
                bufferQueryList.Add(item.Vector);
            }
            Cluster textCluster = Cluster.Compute(bufferTextList, queryVectors.Count);
            Cluster queryCluster = Cluster.Compute(bufferQueryList, queryVectors.Count);
            var scores = queryCluster.GetScores(textCluster);
            double sumScore = 0;
            foreach (Score item in scores)
            {
                sumScore += item.Points;
            }
            var avgScore = sumScore / scores.Count;
            return avgScore;
            //Console.WriteLine("AvgScore in % = {0}", avgScore * 100);

        }
    }
}
