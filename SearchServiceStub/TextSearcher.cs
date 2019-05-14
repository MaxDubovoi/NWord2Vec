using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchServiceStub
{
    public static class TextSearcher
    {
        public static List<string> Search(string query)
        {
            List<string> pathToTextStubs = new List<string>();
            List<string> serchedTexts = new List<string>();
            pathToTextStubs.Add(@"E:\Student\5CourseMaster\Diploma2018\MyDiplomaProject\SampleBooks\My sample text.txt");
            pathToTextStubs.Add(@"E:\Student\5CourseMaster\Diploma2018\MyDiplomaProject\SampleBooks\PrepareToExame.txt");
            foreach(string path in pathToTextStubs)
            {
                serchedTexts.Add(File.ReadAllText(path));
            }
            return serchedTexts;
        }
    }
}
