using DBModelConnector;
using System;

namespace NWord2Vec
{
    public interface IModelReader 
    {
        RealModel Open();
        void LoadToDb();

    }
}
