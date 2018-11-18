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

            Console.WriteLine("Loading Model to Db ");
            var startTime = DateTime.Now;
            var model = new DbModel();
            model.LoadModelToDb(@"model.bin");
            var finishTime = DateTime.Now;
            Console.WriteLine("Time: {0} ", finishTime - startTime);

            Console.WriteLine("Enter word:");
            var word = Console.ReadLine();
            var wordVector = model.ReadWordVector(word);
            Console.WriteLine("Word: {0}  FirstVector: {1}", wordVector.Word, wordVector.Vector.First());


            Console.ReadLine();

            /*
            var model = RealModel.Load(@"model.bin");
            Console.WriteLine("Words loading: "+model.Words);
            while (true)
            {
                Console.WriteLine("Enter word: ");
                var word = Console.ReadLine();
                if (word == "//exit") break;
                Console.WriteLine("Neares: ");
                try
                {
                    List<WordDistance> nearestWords = model.Nearest(word).ToList();
                    foreach (WordDistance element in nearestWords)
                    {
                        Console.WriteLine(element.Word);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
                

            }
        */

        }
    }
}
