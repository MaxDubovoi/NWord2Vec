using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWord2Vec
{
    interface IModel
    {
        IModel Load(string filename, bool isSaveToDatabase = false);
        RealModel Load(IModelReader source);
    }
}
