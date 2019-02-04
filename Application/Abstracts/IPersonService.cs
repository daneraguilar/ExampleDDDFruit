using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Base;
using Application.Implements;

namespace Application.Abstracts
{
    public interface IPersonService : IEntityService<Person>
    {

       CrearPersonResponse NuevaPersona(CrearPersonRequest requets);
    }
}
