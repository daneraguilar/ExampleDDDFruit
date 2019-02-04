using Application.Base;
using Domain.Abstracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implements
{
    class VentaService : EntityService<Venta>, IVentaService
    {

        readonly IUnitOfWork _unitOfWork;
        readonly IVentaRepository _ventaRepository;

        public VentaService(IUnitOfWork unitOfWork, IVentaRepository ventaRepository)
            : base(unitOfWork, ventaRepository)
        {
            _unitOfWork = unitOfWork;
            _ventaRepository = ventaRepository;
        }


        public NuevaVentanResponse NuevaVenta(NuevaVentaRequest requets)
        {

            Venta Venta = _ventaRepository.FindBy(t => t.Id.Equals(requets.Id)).FirstOrDefault();
            if (Venta == null)
            {

                Venta venta = new Venta();

                venta.Id = requets.Id;
                venta.Products = requets.Products;
                venta.Fecha = requets.Fecha;
                venta.Pagos = requets.Pagos;
                venta.calcularTotalVenta();
                _ventaRepository.Add(venta);
                _unitOfWork.Commit();
                return new NuevaVentanResponse()
                {
                    Mensaje = $"La venta fue registrada correctamente"
                };
            }
            else
            {

                return new NuevaVentanResponse()
                {
                    Mensaje = $"Error registrando la venta"
                };
            }

        }

        public DoPagoResponse NuevoPago(DoPagoRequest requets)
        {


            Venta venta = _ventaRepository.FindBy(t => t.Id.Equals(requets.IdVenta)).FirstOrDefault();
            if (venta != null)
            {
                double pagosTotal = 0;

                foreach (Pago p in venta.Pagos) {

                    pagosTotal += p.getValue();

                }

                Pago pago = new Pago();

           
                PagoResponse pagoResponse = pago.SetPagoValue(requets.Value, venta.TotalVentaValue - pagosTotal);
                if (pagoResponse.doPago)
                {
                    venta.Pagos.Add(pago);
                    _ventaRepository.Add(venta);
                    _unitOfWork.Commit();
                    return new DoPagoResponse()
                    {
                        Mensaje = pagoResponse.responseString
                    };


            }else
            {
                    return new DoPagoResponse()
                    {
                        Mensaje = pagoResponse.responseString
                    };

                }
                

              
            }
            else
            {

                return new DoPagoResponse()
                {
                    Mensaje = $"Error Esta Venta no existe"
                };
            }

        }
         }

}
   

    public class NuevaVentaRequest
    {
        public int Id;
        public List<Product> Products;
        public DateTime Fecha;
        public List<Pago> Pagos;


    }
    public class NuevaVentanResponse
    {
        public string Mensaje { get; set; }
    }
    public class DoPagoRequest
    {

        public string IdVenta { get; set; }

        public double Value { get; set; }
       public int Id { get; set; }


}
    public class DoPagoResponse
    {
        public string Mensaje { get; set; }
    }
