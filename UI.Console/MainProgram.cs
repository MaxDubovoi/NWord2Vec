using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModel;

namespace UI.Console
{
    class MainProgram
    {
        
        static void Main(string[] args)
        {
            IView view = new View();
            IModelView modelView = ModelView.BindView(view);
            while (true)
            {
                view.ShowMessage("Enter query for search and <ENTER>: ");
                var userQuery = view.ReadInput();
                modelView.SendQuery(userQuery);
                view.ShowMessage("Press <ENTER> for continue");
            }
           
        }

        
    }
}
