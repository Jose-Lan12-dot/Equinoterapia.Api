using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equinoterapia.Domain.Entities
{
    public class HistorialMedico
    {
        public int Id { get; set; } 
        public int CaballoId { get; set; }
        public int PadecimientoId { get; set; }
        public string? Observaciones { get; set; }


        public Caballo Caballo { get; set; } = null!;
        public Padecimiento Padecimiento { get; set; } = null!;
    }
}
