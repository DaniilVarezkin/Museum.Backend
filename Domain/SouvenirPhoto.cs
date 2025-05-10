using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum.Domain
{
    public class SouvenirPhoto
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public Souvenir? Souvenir { get; set; } = null;

        [ForeignKey(nameof(Souvenir))]
        public Guid SouvenirId { get; set; }
    }
}
