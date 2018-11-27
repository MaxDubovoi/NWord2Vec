﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using System.Globalization;
using NWord2Vec;

namespace NWord2Vec.Tests
{
    [TestClass]
    public class LoadingTests
    {

        [TestMethod]
        public void TestLoadingText()
        {
            var model = RealModel.Load("model.txt");
            TestLoadedModel(model);

        }

        [TestMethod]
        public void TestLoadingTextInAnotherCulture()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
            var model = RealModel.Load("model.txt");
            TestLoadedModel(model);

        }

        [TestMethod]
        public void TestLoadingCompressedText()
        {
            var model = RealModel.Load("model.txt.gz");
            TestLoadedModel(model);
        }


        [TestMethod]
        public void TestReLoadingText()
        {
            var model = RealModel.Load("model.txt");
            RealModel m2;
            using (var s = new MemoryStream())
            {
                using (var writer = new TextModelWriter(s, true))
                {
                    writer.Write(model);
                }
                s.Seek(0, SeekOrigin.Begin);
                var tmr = new TextModelReader(s);
                {
                    m2 = RealModel.Load(tmr);
                }
            }
            Assert.AreEqual(model.Words, m2.Words);
            Assert.AreEqual(model.Size, m2.Size);
        }

        [TestMethod]
        public void TestLoadingBinary()
        {
            var model = RealModel.Load(@"model.bin");
            TestLoadedModel(model);
        }

        [TestMethod]
        public void TestLoadingCompressedBinary()
        {
            var model = RealModel.Load(@"model.bin.gz");
            TestLoadedModel(model);
        }

        [TestMethod]
        public void TestLoadingTextFileWithNoHeader()
        {
            var model = RealModel.Load(@"modelWithNoHeader.txt");
            Assert.AreEqual(2, model.Words);
        }


        [TestMethod]
        public void TestReLoadingBinary()
        {
            var model = RealModel.Load("model.txt");
            RealModel m2;
            using (var s = new MemoryStream())
            {
                using (var writer = new BinaryModelWriter(s, true))
                {
                    writer.Write(model);
                }
                s.Seek(0, SeekOrigin.Begin);
                var tmr = new BinaryModelReader(s);
                m2 = RealModel.Load(tmr);
            }
            Assert.AreEqual(model.Words, m2.Words);
            Assert.AreEqual(model.Size, m2.Size);
        }

      

        private static void TestLoadedModel(RealModel model)
        {
            Assert.IsNotNull(model);
            Assert.AreEqual(4501, model.Words);
            Assert.AreEqual(100, model.Size);
            Assert.AreEqual(4501, model.Vectors.Count());
            Assert.IsTrue(model.Vectors.Any(x => x.Word == "whale"));


            var whale = model.GetByWord("whale");
            Assert.IsNotNull(whale);

            var xyz = model.GetByWord("xyz");
            Assert.IsNull(xyz);

            var results = model.Nearest(whale.Vector).Take(10).ToArray();
            Assert.AreEqual(10, results.Length);
            Assert.AreEqual("whale", results[0].Word);

            var results2 = model.Nearest("whale").Take(10).ToArray();
            Assert.AreEqual(10, results2.Length);
            Assert.AreNotEqual("whale", results2[0].Word);
            Assert.AreEqual("whale,", results2[0].Word);

            var nearest = model.NearestSingle(model.GetByWord("whale").Subtract(model.GetByWord("sea")));
            Assert.IsNotNull(nearest);

            Assert.AreNotEqual(0, model.Distance("whale", "boat"));



            var king = model.GetByWord("whale");
            var man = model.GetByWord("boat");
            var woman = model.GetByWord("sea");

            var vector = king.Subtract(man).Add(woman);
            Console.WriteLine(model.NearestSingle(vector));
        }

        [TestMethod]
        public void TestVectorAddition()
        {
            var x = new float[] { 1, 2, 3 };
            var result = x.Add(x);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(4, result[1]);
            Assert.AreEqual(6, result[2]);
        }
        [TestMethod]
        public void TestVectorPower()
        {
            var x = new float[] { 1, 2, 3 };
            var result = x.Pow(2);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(4, result[1]);
            Assert.AreEqual(9, result[2]);
        }

        [TestMethod]
        public void TestVectorSubtraction()
        {
            var x = new float[] { 1, 2, 3 };
            var result = x.Subtract(x);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(0, result[1]);
            Assert.AreEqual(0, result[2]);
        }

        [TestMethod]
        public void TestVectorDistance()
        {
            var x = new float[] { 1, 3, 4 };
            var y = new float[] { 1, 0, 0 };
            var result = x.Distance(y);
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void TestVectorAdditionOperator()
        {
            var x = new WordVector("word1", new float[] { 1, 3, 4 });
            var y = new WordVector("word2", new float[] { 1, 0, 0 });
            var z = x + y;

            Assert.IsNotNull(z);
            CollectionAssert.AreEqual(new float[] { 2, 3, 4 }, z);
        }

        [TestMethod]
        public void TestVectorSubtractionOperator()
        {
            var x = new WordVector("word1", new float[] { 1, 3, 4 });
            var y = new WordVector("word2", new float[] { 1, 0, 0 });
            var z = y + x - y;

            Assert.IsNotNull(z);
            CollectionAssert.AreEqual(new float[] { 1, 3, 4 }, z);
        }


    }
}
