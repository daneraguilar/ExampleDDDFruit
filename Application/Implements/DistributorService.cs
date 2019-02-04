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
    public class DistributorService  : EntityService<Distributor>, IDistributorService

    { 

    readonly IUnitOfWork _unitOfWork;
    readonly IDistributorRepository _distributorRepository;

    public DistributorService(IUnitOfWork unitOfWork, IDistributorRepository distributorRepository)
            : base(unitOfWork, distributorRepository)
        {
        _unitOfWork = unitOfWork;
        _distributorRepository = distributorRepository;
    }


    public CrearDistributortResponse NuevoDistributor(CrearDistributortRequest requets)
    {

        Distributor distributor = _distributorRepository.FindBy(t => t.Nit.Equals(requets.Nit)).FirstOrDefault();
        if (distributor == null)
        {

            Distributor newProduct = new Distributor();

                distributor.Nit = requets.Nit;
                distributor.Name = requets.Name;
                distributor.Credit = requets.Credit;
                distributor.Credit = requets.Credit;

                _distributorRepository.Add(distributor);
            _unitOfWork.Commit();
            return new CrearDistributortResponse()
            {
                Mensaje = $"El Distribuidor  fue registrado correctamente"
            };
        }
        else
        {

            return new CrearDistributortResponse()
            {
                Mensaje = $"El Distribuidor con este Nit  ya existe"
            };
        }

    }

}

public class CrearDistributortRequest
{

     
        public string Nit;

        public string Name;
      
        public double Credit;
      
        public string direcction;
    }
public class CrearDistributortResponse
    {
    public string Mensaje { get; set; }
}
}


