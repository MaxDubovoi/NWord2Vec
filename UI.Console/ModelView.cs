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
            var n1GrammQuery = TextConvertor.TextToWordList(query);
            var texts = TextSearcher.Search(query);
            foreach (string text in texts)
            { 
                var n1GrammText = TextConvertor.TextToWordList(text);
                var vectorizedText = _word2VecModelDB.CreateWordVectorList(n1GrammText);
                var vectorizedQuery = _word2VecModelDB.CreateWordVectorList(n1GrammQuery);
                var score = Cluster.GetAccordance(WordVector.GetVectors(vectorizedText), WordVector.GetVectors(vectorizedQuery));
                _view.ShowTextWithScore(text, score);
            }


        }
    }
}
