using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equinoterapia.Application.DTOs.Archivos
{
    public class GetByIdArchivoDto
    {
        public byte[] Archivo { get; set; } = null!;
        public string Extension { get; set; } = string.Empty;
        
    }
}
