using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModel;

namespace UI.Console
{
    class View:IView
    { 
        public void ShowQuery(string query) => System.Console.Write("Your query: {0}/n", query);


        public void ShowTextWithScore(string text, double score)
        { 
            var shortDescription = CreateShortTextDescription(text, 3);
            System.Console.Write("Text: \n\n{0}..\n\n", shortDescription);
            System.Console.Write("Truth:\t {0:f} %\n", score*100);
        }
        public void ShowMessage(string message)
        {
            System.Console.Write(message);
        }
        public string ReadInput()
        {
            return System.Console.ReadLine();
        }
        private string CreateShortTextDescription(string fullText, int numberOfSentences)
        {
            var sentences = fullText.Split('.');
            StringBuilder shortDescriptionBuilder = new StringBuilder();
            for(int i = 0; i<sentences.Count(); i++)
            {
                if (i > numberOfSentences) break;
                shortDescriptionBuilder.Append(sentences.ElementAt(i));
            }
            return shortDescriptionBuilder.ToString();
        }
    }
}
