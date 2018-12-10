﻿using System;
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
            Console.WriteLine("Connected");
            
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
            string path = @"E:\Student\5CourseMaster\Diploma2018\MyDiplomaProject\SampleBooks\My sample text.txt"; 
            Console.WriteLine("Enter path to txt file: ", path);
            path = Console.ReadLine();
            var wordVectorTextList = model.CreateWordVectorList(path);
            var textModel = new RealModel(wordVectorTextList.Count, wordVectorTextList.First().Vector.Length, wordVectorTextList);
            while (true)
            { 
                Console.Write("Enter main theme: ");
                string word = Console.ReadLine();
                var wordVector = model.ReadWordVector(word);
                if(wordVector.Vector.Count()<300)
                {
                    Console.WriteLine("Unknown word");
                    continue;
                }
               
                var word2textDistanceList = textModel.GetWordDistances(wordVector);
                WordVector centralTextVector = new WordVector("centralVector", textModel.GetCentralVector());
                var centre2textDistanceList = textModel.GetWordDistances(centralTextVector);
                Console.WriteLine("Distance list computed!");

                var relativeDistance = 1 - (word2textDistanceList.MaxDistance() - word2textDistanceList.AvarageDistance())/ word2textDistanceList.MaxDistance();
                Console.WriteLine("Avarage Distance with {0}: {1}",word, word2textDistanceList.AvarageDistance());
                Console.WriteLine("Avarage Distance with {0}: {1}", centralTextVector.Word, centre2textDistanceList.AvarageDistance());
                Console.WriteLine("Relative Distance with {0}: {1}", word, relativeDistance);
                Console.WriteLine("Absolute Distance to central vector: {0}", wordVector.Vector.Distance(centralTextVector.Vector));
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
                model.LoadModelToDb(@"E:\Student\5CourseMaster\Diploma2018\Example program\Models\GoogleNews-vectors-negative300.bin");
                var finishTime = DateTime.Now;
                Console.WriteLine("Time: {0} ", finishTime - startTime);
                Console.ReadLine();
            }


        }
    }
}
