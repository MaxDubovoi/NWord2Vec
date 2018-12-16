using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Console
{
    public interface IView
    {
        void ShowQuery(string query);
        void ShowTextWithScore(string text, double score);
        void ShowMessage(string message);
        string ReadInput();
    }
}
