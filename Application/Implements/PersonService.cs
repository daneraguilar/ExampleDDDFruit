using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Base;
using Application.Contracts;
using Domain.Abstracts;
using Domain.Entities;
using Application.Abstracts;

namespace Application.Implements
{
    public class PersonService : EntityService<Person>,IPersonService
    {

        readonly IUnitOfWork _unitOfWork;
        readonly IPersonRepository _personRepository;

        public PersonService(IUnitOfWork unitOfWork, IPersonRepository personRepository)
            : base(unitOfWork, personRepository)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
        }


        public CrearPersonResponse NuevaPersona(CrearPersonRequest requets)
        {

            Person person = _personRepository.FindBy(t => t.Document.Equals(requets.Document)).FirstOrDefault();
            if(person == null)
            {

                Person newPerson = new Person();

                newPerson.Name = requets.Name;
                newPerson.Address = requets.Address;
                newPerson.CountryId = requets.CountryId;
                newPerson.Document = requets.Document;
                newPerson.Phone = requets.Phone;

                _personRepository.Add(newPerson);
                _unitOfWork.Commit();
                return new CrearPersonResponse()
                {
                    Mensaje = $"El Usuario fue registrado correctamente"
                };
            }
            else
            {

                return new CrearPersonResponse()
                {
                    Mensaje = $"El Usuario con este numero de documento ya existe"
                };
            }
            
        }

    }

    public class CrearPersonRequest
    {
       
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        public long Document { get; set; }

        public int CountryId { get; set; }
        
    }
    public class CrearPersonResponse
    {
        public string Mensaje { get; set; }
    }
}
