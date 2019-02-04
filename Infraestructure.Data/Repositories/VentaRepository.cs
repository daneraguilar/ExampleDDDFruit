using Domain.Abstracts;
using Domain.Entities;
using Infraestructura.Data.Base;
using SirccELC.Infraestructura.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Repositories
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        public VentaRepository(IDbContext context)
              : base(context)
        {

        }

    }
}
