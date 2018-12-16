using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchServiceStub
{
    public static class TextSearcher
    {
        public static string Search(string query)
        {
            List<string> pathToTextStub = new List<string>();
            pathToTextStub.Add(@"E:\Student\5CourseMaster\Diploma2018\MyDiplomaProject\SampleBooks\My sample text.txt");
            pathToTextStub.Add(@"E:\Student\5CourseMaster\Diploma2018\MyDiplomaProject\SampleBooks\PrepareToExame.txt");
            var rand = new Random();
            var textIndex = rand.Next(0, 1);
            return File.ReadAllText(pathToTextStub.GetRange(textIndex, 1).First());
        }
    }
}
