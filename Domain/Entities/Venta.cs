using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Venta  : Entity<int>,IServicioVenta
    {

        public List<Product> Products;
        public DateTime Fecha;
        public List<Pago> Pagos;
        public double TotalVentaValue;

        public void calcularTotalVenta()
        {
            foreach (Product p in this.Products) {

                this.TotalVentaValue += p.PriceToBuy * p.Quantity;

            }
        }
    }
}
