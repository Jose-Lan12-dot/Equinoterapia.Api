using APIproyectoEquinoterapia.Data.Models;
using APIproyectoEquinoterapia.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace APIproyectoEquinoterapia.Data.Services
{
    public class SesionService
    {
        private readonly AppDbContext _context;

        public SesionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SesionDTO>> GetSesionesAsync()
        {
            return await _context.sesion
                .Select(s => new SesionDTO
                {
                    SesionId = s.SesionId,
                    InstructorId = s.InstructorId,
                    Placa = s.Placa,
                    Fecha = s.Fecha,
                    Hora = s.Hora
                })
                .ToListAsync();
        }

        public async Task<SesionDTO?> GetSesionAsync(int id)
        {
            var sesion = await _context.sesion.FindAsync(id);
            if (sesion == null)
                return null;

            return new SesionDTO
            {
                SesionId = sesion.SesionId,
                InstructorId = sesion.InstructorId,
                Placa = sesion.Placa,
                Fecha = sesion.Fecha,
                Hora = sesion.Hora
            };
        }

        public async Task<bool> PostSesionAsync(SesionDTO sesionDTO)
        {
            var sesion = new Sesion
            {
                InstructorId = sesionDTO.InstructorId,
                Placa = sesionDTO.Placa,
                Fecha = sesionDTO.Fecha,
                Hora = sesionDTO.Hora
            };

            _context.sesion.Add(sesion);
            await _context.SaveChangesAsync();
            sesionDTO.SesionId = sesion.SesionId;
            return true;
        }

        public async Task<bool> PutSesionAsync(int id, SesionDTO sesionDTO)
        {
            var sesion = await _context.sesion.FindAsync(id);
            if (sesion == null)
                return false;

            sesion.InstructorId = sesionDTO.InstructorId;
            sesion.Placa = sesionDTO.Placa;
            sesion.Fecha = sesionDTO.Fecha;
            sesion.Hora = sesionDTO.Hora;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSesionAsync(int id)
        {
            var sesion = await _context.sesion.FindAsync(id);
            if (sesion == null)
                return false;

            _context.sesion.Remove(sesion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}