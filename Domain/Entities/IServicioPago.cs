using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    interface IServicioPago
    {

        PagoResponse SetPagoValue(double pagoValue, double deudaTotal );
    }

    public class PagoResponse
    {

        public String responseString;
        public bool doPago;
    }
}
