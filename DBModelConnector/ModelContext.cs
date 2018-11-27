using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModelConnector
{
    class ModelContext : DbContext
    {
        public ModelContext():base("WikipediaWord2VecModel")//Word2VecModel
        {

        }
        public DbSet<WordDTO> Words { get; set; }
        public DbSet<VectorDTO> Vectors { get; set; }

        public class WordDTO
        {
            public int Id { get; set; }
            public string Value { get; set; }
            public ICollection<VectorDTO> Vectors { get; set; }
        }

        public class VectorDTO
        {
            public int Id { get; set; }
            public int VectorIndex { get; set; }
            public float Value { get; set; }
            public virtual WordDTO WordDTO { get; set; }

        }
        public class ListVectorDTO
        {   
            public double vector { get; set; }
        }
        
    }
}
