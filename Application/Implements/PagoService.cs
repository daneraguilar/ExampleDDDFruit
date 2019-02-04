using Application.Abstracts;
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
    public class PagoService : EntityService<Pago>, IPagoService 
    {

        readonly IUnitOfWork _unitOfWork;
        readonly IPagoRepository _pagoRepository;

        public PagoService(IUnitOfWork unitOfWork, IPagoRepository pagoRepository)
            : base(unitOfWork, pagoRepository)
        {
            _unitOfWork = unitOfWork;
            _pagoRepository = pagoRepository;
        }

        /*
        public DoPagoResponse NewPago(DoPagoRequest requets)
        {

            Pago venta = _pagoRepository.FindBy(t => t.Id.Equals(requets.)).FirstOrDefault();
            if (person == null)
            {

                Person newPerson = new Person();

                newPerson.Name = requets.Name;
                newPerson.Address = requets.Address;
                newPerson.CountryId = requets.CountryId;
                newPerson.Document = requets.Document;
                newPerson.Phone = requets.Phone;

                _pagoRepository.Add(newPerson);
                _unitOfWork.Commit();
                return new DoPagoResponse()
                {
                    Mensaje = $"El Pago fue registrado correctamente"
                };
            }
            else
            {

                return new DoPagoResponse()
                {
                    Mensaje = $"El Pago con este numero de documento ya existe"
                };
            }

        }*/

    }

    public class DoPagoRequest
    {

        public string IdVenta { get; set; }

        public double Value { get; set; }


    }
    public class DoPagoResponse
    {
        public string Mensaje { get; set; }
    }
}

