//using APIproyectoEquinoterapia.Data.Models;
//using APIproyectoEquinoterapia.DTOs;
//using Microsoft.EntityFrameworkCore;

//namespace APIproyectoEquinoterapia.Data.Services
//{
//    public class AsistenciaService
//    {
//        private readonly AppDbContext _context;

//        public AsistenciaService(AppDbContext context)
//        {
//            _context = context;
//        }


//        public async Task<(IEnumerable<GetAsistenciaDto>,string)> GetAsync(int sesionId)
//        {
//            var asistencias = new List<GetAsistenciaDto>();
            
//            if (!await SesionesExist(sesionId))
//            {
//                return (asistencias,
//                    "La sesion especificada no existe");
//            }

//            try
//            {
//                asistencias = await GetAsistencias(sesionId);
//            }
//            catch (Exception ex)
//            {
//                return (asistencias,
//                    $"Error al obtener los pacientes participantes: {ex.Message}");
//            }

//            if (asistencias.Count == 0)
//            {
//                return (asistencias,
//                    "No se encontraron pacientes para la sesion especificada");
//            }

//            return (asistencias, "Participantes devueltos correctamente");
//        }

//        public async Task<( string, bool)> AddAsync(AddAsistenciaDto asistenciaAdd)
//        {
//            var sesionId = asistenciaAdd.SesionId;
//            var pacientesIds = asistenciaAdd.PacientesId;

//            if (await SesionesExist(sesionId))
//            {
//                var mensaje = "Sesion no existe";
//                return (mensaje, false);
//            }
//            try
//            {
//                await AddPacientes(pacientesIds,sesionId);
//                return ( "Pacientes agregados correctamnete", true);
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al agregar los pacientes: {ex.Message}";
//                return ( mensaje, false);
//            }

//        }

//        public async Task<(string, bool)> UpdateAsync(UpdateAsistenciaDto asistenciaUpdate)
//        {
//            try
//            {
//                var asitenciasExistentes = await GetAsistenciasExistentes(asistenciaUpdate.Asistencias);

//                foreach (var asistenciaItem in asitenciasExistentes)
//                {
//                    var asistencia = await _context.asistencia.FindAsync(asistenciaItem.AsistenciasId);
//                    if (asistencia != null)
//                    {
//                        asistencia.Asistio = asistenciaItem.Asistio;
//                    }
//                }
//                await _context.SaveChangesAsync();
//                return ("Asistencias actualizadas correctamente", true);
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al actualizar las asistencias: {ex.Message}";
//                return (mensaje, false);
//            }
//        }

//        public async Task<(string, bool)> DelateAsync(List<int> asistenciaId) 
//        {
//            if (asistenciaId == null || asistenciaId.Count == 0)
//            {
//                return ("No se proporcionaron IDs de asistencias para eliminar", false);
//            }

//            try
//            {
//                foreach (var id in asistenciaId)
//                {
//                    var asistencia = await _context.asistencia.FindAsync(id);
//                    if (asistencia != null)
//                    {
//                        _context.asistencia.Remove(asistencia);
//                    }
//                }
//                await _context.SaveChangesAsync();
//                return ("Asistencias eliminadas correctamente", true);
//            }
//            catch (Exception ex)
//            {
//                var mensaje = $"Error al eliminar las asistencias: {ex.Message}";
//                return (mensaje, false);
//            }
//        }








//        public async Task AddPacientes(List<int> pacientesIds, int sesionId)
//        {
//            var pacientesExistentes = await GetPacientesExistentes(pacientesIds);

//            foreach (var pacienteId in pacientesExistentes)
//            {
//                    var nuevaAsitencia = new Asistencia()
//                    {
//                        SesionId = sesionId,
//                        PacienteId = pacienteId,
//                        Asistio = false
//                    };
//                    _context.asistencia.AddInforme(nuevaAsitencia);
//            }
//            await _context.SaveChangesAsync();
//        }

//        private async Task<List<GetAsistenciaDto>> GetAsistencias(int sesionId)
//        {
//            var consulta = await _context.asistencia
//                .Where(a => a.SesionId == sesionId)
//                .ToListAsync();

//            return await MapToDto(consulta);
//        }

//        private async Task<List<GetAsistenciaDto>> MapToDto(List<Asistencia> asistencias)
//        {

//            var lista = new List<GetAsistenciaDto>();
//            foreach (var asistencia in asistencias)
//            {
//                var nombrePaciente = await GetNombrePaciente(asistencia.PacienteId);
//                lista.AddInforme(new GetAsistenciaDto
//                {
//                    AsistenciaId = asistencia.AsistenciaId,
//                    nombre = nombrePaciente ?? string.Empty,
//                    Asistio = asistencia.Asistio
//                });
//            }
//            return lista;
//        }

//        private async Task<string?> GetNombrePaciente(int pacienteId)
//        {
//            var paciente = await _context.paciente
//                .Where(p => p.PacienteId == pacienteId)
//                .Select(p => p.Nombre)
//                .FirstOrDefaultAsync();
//            return paciente;
//        }

//        private async Task<bool> SesionesExist(int sesionId)
//        {
//           return await _context.sesion.AnyAsync(s => s.SesionId == sesionId);
//        }

//        private async Task<List<int>> GetPacientesExistentes(List<int> pacientesIds)
//        {
//            return await _context.paciente
//                .Where(p => pacientesIds.Contains(p.PacienteId))
//                .Select(p => p.PacienteId)
//                .ToListAsync();
//        }

//        private async Task<List<UpdateAsistenciaItemDto>> GetAsistenciasExistentes(List<UpdateAsistenciaItemDto> asistencias)
//        {
//            var asistenciasIds = asistencias.Select(a => a.AsistenciasId).ToList();
//            var consulta = await _context.asistencia
//                .Where(p => asistenciasIds.Contains(p.AsistenciaId))
//                .Select(a => a.AsistenciaId)
//                .ToListAsync();

//            var asistenciasExistentes = asistencias.Where(a => consulta.Contains(a.AsistenciasId)).ToList();
//            return asistenciasExistentes;
//        }

//    }
//}
