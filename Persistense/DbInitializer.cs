using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Persistense
{
    public class DbInitializer
    {
        public static void Initialize(MuseumDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
