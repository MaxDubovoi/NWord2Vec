using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DBModelConnector
{
    public class DbModelConnector
    {
        private static DbModelConnector connection;
        private ModelContext context;

        private DbModelConnector()
        {
            
        }
        public static DbModelConnector GetConnector()
        {
            if(connection == null)
            {
                connection = new DbModelConnector();
            }

            return connection;
        }

        public void AddVector(string word, float[] vector )
        {
                if(context == null)
            {
                context = new ModelContext();
                context.Configuration.AutoDetectChangesEnabled = false;
            }
           
                var wordDTO = new ModelContext.WordDTO { Value = word };
                var vectors = new List<ModelContext.VectorDTO>();

               for(int i = 0; i<vector.Length; i++)
                {
                    vectors.Add(new ModelContext.VectorDTO { Value = vector[i], WordDTO = wordDTO });
                }
            wordDTO.Vectors = vectors;
            context.Vectors.AddRange(wordDTO.Vectors);
            context.Words.Add(wordDTO);
                

        }

        public void ClearDb()
        {
                context = new ModelContext();
                context.Database.Delete();
        }

        public float[] GetVector(string word)
        {
            using (var modelContext = new ModelContext())
            {
                var vectors = modelContext.Vectors.Where(v => v.WordDTO.Value == word).Select(v => v.Value);
                
                return vectors.ToArray();
            }
        }
        public void SaveChanges()
        {
            if (context != null)
            {
                context.SaveChanges();
                context.Dispose();
                context = null;
            }
        }


    }
}
