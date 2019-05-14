using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using simple_text_mining_library;
using Clusteriser;
using Clusteriser.DTO;
using NWord2Vec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModel;

namespace UnitTests
{
    /// <summary>
    /// Сводное описание для UnitTest2
    /// </summary>
    [TestClass]
    public class ModelViewTest
    {
        
      
        //TODO: Rewrite
        [TestMethod]
        public void TextToWordList()
        {
            List<string> textList = new List<string> { "I saw a cat and a horse.???", "Google searches the Internet{}{}{[][][][[[])(-091)", "Using an extra step to eliminate stopwords" };
            List<string> expectList = new List<string> { "i cat horse ", "google searches internet ", "using extra step eliminate stopwords" };
            for (int i = 0; i < textList.Count; i++)
            {
                string proccesedText;
                TextConvertor textConvertor = new TextConvertor();
                textConvertor.textMiningLanguage = simple_text_mining_library.Classes.TextMiningLanguage.English;
                proccesedText = textConvertor.RemoveStopWordsFromText(textList[i]);
                proccesedText = textConvertor.RemoveSpecialCharacters(proccesedText, true);
                Assert.AreEqual(proccesedText, expectList[i]);
            }
        }
    }
}
