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
                    vectors.Add(new ModelContext.VectorDTO {VectorIndex = i, Value = vector[i], WordDTO = wordDTO });
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
                float[] vectors = null;
                double[] result;

                    System.Data.SqlClient.SqlParameter paramW1 = new System.Data.SqlClient.SqlParameter("@enterWord", word);
                   vectors = modelContext.Database.SqlQuery<float>("dbo.GetVectorByWord @enterWord", paramW1).ToArray();
                    //vectors = result.ToFloat();
                    return vectors;
            }
        }
        public double GetDistanceProcedure(string word1, string word2)
        {
            using (var modelContext = new ModelContext())
            {
                double distance = 0;
                System.Data.SqlClient.SqlParameter paramW1 = new System.Data.SqlClient.SqlParameter("@word1", word1);
                System.Data.SqlClient.SqlParameter paramW2 = new System.Data.SqlClient.SqlParameter("@word2", word2);
                try { 
                var result = modelContext.Database.SqlQuery<double>("dbo.GetDistance @word1, @word2", paramW1, paramW2);
                distance = result.FirstOrDefault();
                
                }
                catch (Exception ex)
                {
                    distance = -1;
                }
                return distance;
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
