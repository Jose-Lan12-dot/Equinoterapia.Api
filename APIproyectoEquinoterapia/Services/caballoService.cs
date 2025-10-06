using APIproyectoEquinoterapia.DTOs;
using APIproyectoEquinoterapia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoEquinoterapia.Services

{ 

    public class CaballoService
    {


        private readonly AppDbContext _context;

        public CaballoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CaballoDTO>> GetCaballosAsync()
        {

            return await _context.caballo
                    .Select(c => new CaballoDTO
                    {
                        Placa = c.Placa,
                        Nombre = c.Nombre,
                        Estado = c.Estado,
                        FechaHerraje = c.FechaHerraje,
                        Padecimiento = c.Padecimiento,
                    })
                    .ToListAsync();
        }

        public async Task<CaballoDTO?> GetCaballoAsync(string placa) 
        {

            var caballo = await _context.caballo.FindAsync(placa);
            if (caballo == null)
                return null;

            return new CaballoDTO
            {
                Placa = caballo.Placa,
                Nombre = caballo.Nombre,
                Estado = caballo.Estado,
                FechaHerraje = caballo.FechaHerraje,
                Padecimiento = caballo.Padecimiento,
            };
        }


        public async Task<bool> PutCaballoAsync(string placa, CaballoDTO caballoDTO)
        {
            var caballo = await _context.caballo.FindAsync(placa);
            if (caballo == null)
                return false;

            // Actualiza los campos
            caballo.Nombre = caballoDTO.Nombre;
            caballo.Estado = caballoDTO.Estado;
            caballo.FechaHerraje = caballoDTO.FechaHerraje;
            caballo.Padecimiento = caballoDTO.Padecimiento;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostCaballoAsync(CaballoDTO caballoDTO) 
        {

            if (!_context.caballo.Any(c => c.Placa == caballoDTO.Placa)) 
            {
                return false;
            }

            var nuevoCaballo = new Caballo
            {
                Placa = caballoDTO.Placa,
                Nombre = caballoDTO.Nombre,
                Estado = caballoDTO.Estado,
                FechaHerraje = caballoDTO.FechaHerraje,
                Padecimiento = caballoDTO.Padecimiento,
            };

            _context.caballo.Add(nuevoCaballo);
            await _context.SaveChangesAsync();
            return true;
      
           
        }


        public async Task<bool> DeleteCaballoAsync(string placa) 
        {
            var caballo = await _context.caballo.FindAsync(placa);
            if (caballo == null)
                return false;
            _context.caballo.Remove(caballo);
            await _context.SaveChangesAsync();
            return true;
        }




    }
}

