using Equinoterapia.Application.DTOs.Informes;
using Equinoterapia.Application.Interfaces;
using Equinoterapia.Domain.Entities;
using Equinoterapia.Domain.Interfaces;

namespace Equinoterapia.Application.Services
{
    public class InformeService : IInformeService
    {
        private readonly IInformeRepository _repository;

        public InformeService(IInformeRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetEstadisticasDto> GetEstadisticasAsync(int mes, int anio)
        {
            var terapias = await _repository.GetTotalTerapiasAsync(mes, anio);
            var ingresos = await _repository.GetNuevosIngresosAsync(mes, anio);
            return new GetEstadisticasDto { TotalTerapias = terapias, NuevosIngresos = ingresos };
        }

        public async Task<InformeDto> GetInformeAsync(int mes, int anio)
        {
            var informeBd = await _repository.GetInformeAsync(mes, anio);

            if (informeBd != null)
            {
                return new InformeDto
                {
                    Mes = informeBd.Mes,
                    Anio = informeBd.Anio,
                    numeroTarjeta = informeBd.numeroTarjeta,
                    Detalles = informeBd.Detalles.Select(d => new DetalleDto
                    {
                        NumeroOrden = d.NumeroOrden,
                        Situacion = d.Situacion,
                        Acciones = d.Acciones,
                        Avances = d.Avances,
                        Tramite = d.Tramite,
                        Observaciones = d.Observaciones
                    }).ToList()
                };
            }


            var stats = await GetEstadisticasAsync(mes, anio);

            var informeSeed = new InformeDto { Mes = mes, Anio = anio, numeroTarjeta = string.Empty };

            informeSeed.Detalles.Add(new DetalleDto
            {
                NumeroOrden = 1,
                Situacion = "Aumentar el número total de terapias en el año",
                Acciones = "Disminuir las cancelaciones.",
                Avances = $"En el mes se contó con un total de {stats.TotalTerapias} terapias.",
                Tramite = "",
                Observaciones = ""
            });

            informeSeed.Detalles.Add(new DetalleDto
            {
                NumeroOrden = 2,
                Situacion = "Altas de Nuevo Ingreso",
                Acciones = $"Se tuvo el ingreso de {stats.NuevosIngresos} niños nuevos",
                Avances = "",
                Tramite = "",
                Observaciones = ""
            });

            return informeSeed;
        }

        public async Task<(bool, string)> GuardarInformeAsync(InformeDto dto)
        {
            try
            {
                var informeExistente = await _repository.GetInformeAsync(dto.Mes, dto.Anio);

                if (informeExistente != null)
                {
                    informeExistente.FechaModificacion = DateTime.Now;
                    informeExistente.numeroTarjeta = dto.numeroTarjeta;

                    if (informeExistente.Detalles.Any())
                    {
                        _repository.RemoveDetalles(informeExistente.Detalles);
                    }

                    informeExistente.Detalles = dto.Detalles.Select(d => new DetalleInforme
                    {
                        NumeroOrden = d.NumeroOrden,
                        Situacion = d.Situacion,
                        Acciones = d.Acciones,
                        Avances = d.Avances,
                        Tramite = d.Tramite,
                        Observaciones = d.Observaciones
                    }).ToList();
                }
                else
                {
                    var nuevoInforme = new InformeMensual
                    {
                        Mes = dto.Mes,
                        Anio = dto.Anio,
                        numeroTarjeta = dto.numeroTarjeta,
                        FechaModificacion = DateTime.Now,
                        Detalles = dto.Detalles.Select(d => new DetalleInforme
                        {
                            NumeroOrden = d.NumeroOrden,
                            Situacion = d.Situacion,
                            Acciones = d.Acciones,
                            Avances = d.Avances,
                            Tramite = d.Tramite,
                            Observaciones = d.Observaciones
                        }).ToList()
                    };
                    _repository.AddInforme(nuevoInforme);
                }

                await _repository.SaveChangesAsync();
                return (true, "Informe guardado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al guardar el informe: {ex.Message}");
            }
        }
    }
}