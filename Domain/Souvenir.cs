using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Domain
{
    public class Souvenir
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public SouvenirPhoto? Photo { get; set; } = null;
    }
}
