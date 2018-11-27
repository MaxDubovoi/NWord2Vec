using simple_text_mining_library;
using simple_text_mining_library.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace simple_text_mining_library_sample_app
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SampleBooks\My simple text.txt");
            string sampleInputText = File.ReadAllText(filePath);
            /*Console.WriteLine("Enter text:\n");
            sampleInputText = Console.ReadLine();*/
            MineText mineText = new MineText();
            mineText.textMiningLanguage = TextMiningLanguage.English;

            string a = mineText.RemoveSpecialCharacters(sampleInputText, true);
            string b = mineText.RemoveSpecialCharacters(sampleInputText, false);
            string c = sampleInputText;

            string d = mineText.RemoveStopWordsFromText(a);

            List<string> e = mineText.N1GramAnalysis(d);
            List<string> f = mineText.N2GramAnalysis(d);
            List<string> g = mineText.N3GramAnalysis(d);
            List<string> h = mineText.N4GramAnalysis(d);
            List<string> i = mineText.N5GramAnalysis(d);
            e.ForEach(item => Console.Write("{0}\n", item));
            Console.Write("End");
            Console.ReadLine();
        }
    }
}
