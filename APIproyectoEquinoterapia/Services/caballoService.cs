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

        public async Task<(IEnumerable<CaballoDTO>,string)> GetCaballosAsync()
        {
            var caballos = await _context.caballo
                    .Select(c => new CaballoDTO
                    {
                        Placa = c.Placa,
                        Nombre = c.Nombre,
                        Estado = c.Estado.ToString(),
                        FechaHerraje = c.FechaHerraje,
                        Padecimiento = c.Padecimiento,
                    })
                    .ToListAsync();

            if (caballos == null || !caballos.Any())
            {
                var mensaje = "No se encontro ningun caballo";
                return (Enumerable.Empty<CaballoDTO>(), mensaje);
            }



            return (caballos,"Lista de caballos devulelta satisfactoriamente");
        }

        public async Task<(CaballoDTO?,string)> GetCaballoAsync(string placa) 
        {

            var caballo = await _context.caballo.FindAsync(placa);
            if (caballo == null)
            {
                var mensaje = "Caballo no encontrado";
                return (null,mensaje);
            }

            return (new CaballoDTO
            {
                Placa = caballo.Placa,
                Nombre = caballo.Nombre,
                Estado = caballo.Estado.ToString(),
                FechaHerraje = caballo.FechaHerraje,
                Padecimiento = caballo.Padecimiento,
            },"Caballo devulelta satisfactoriamente");
        }


        public async Task<(bool,string)> PutCaballoAsync(string placa, CaballoDTO caballoDTO)
        {
            var caballo = await _context.caballo.FindAsync(placa);
            if (caballo == null)
            {
                var mensaje = "Caballo no encontrado";
                return (false,mensaje);
            }

            if (!Enum.TryParse<EstadoCaballo>(caballoDTO.Estado, out var estadoValido))
            {
                var mensaje = "Estado no valido";
                return (false,mensaje);
            }

            caballo.Nombre = caballoDTO.Nombre ?? caballo.Nombre;
            caballo.Estado = estadoValido;
            caballo.FechaHerraje = caballoDTO.FechaHerraje;
            caballo.Padecimiento = caballoDTO.Padecimiento;

            await _context.SaveChangesAsync();
            return (false,"Caballo "+placa+" actualizado correctamente");
        }

        public async Task<(bool,string)> PostCaballoAsync(CaballoDTO caballoDTO) 
        {

            if (caballoDTO.Placa == null || caballoDTO == null) 
            {
                var mensaje = "La placa es obligatoria";
                return (false, mensaje);
            }

            if (!_context.caballo.Any(c => c.Placa == caballoDTO.Placa)) 
            {
                var mensaje = "Ya existe un caballo con esa placa";
                return (false, mensaje);
            }

            if (!Enum.TryParse<EstadoCaballo>(caballoDTO.Estado, out var estadoValido))
            {
                var mensaje = "Estado no valido";
                return (false, mensaje);
            }


            var nuevoCaballo = new Caballo
            {
                Placa = caballoDTO.Placa,
                Nombre = caballoDTO.Nombre ?? "",
                Estado = estadoValido,
                FechaHerraje = caballoDTO.FechaHerraje,
                Padecimiento = caballoDTO.Padecimiento,
            };

            _context.caballo.Add(nuevoCaballo);
            await _context.SaveChangesAsync();
            return (true,"Caballo " +caballoDTO.Placa + " agregado correctamente");
      
           
        }


        //public async Task<bool> DeleteCaballoAsync(string placa) 
        //{
        //    var caballo = await _context.caballo.FindAsync(placa);
        //    if (caballo == null)
        //        return false;
        //    _context.caballo.Remove(caballo);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}




    }
}

