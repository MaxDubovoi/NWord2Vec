using System;
using System.Collections.Generic;
using System.Text;
using UI.Console;

namespace UI.ViewModel
{
    public interface IModelView
    {
        void SendQuery(string query);
    }
}
