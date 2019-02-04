using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Pago : Entity<int>, IServicioPago
    {

        private double Value;
        public DateTime fecha;


        public double getValue()
        {
            return Value;
        }

        public PagoResponse SetPagoValue(double pagoValue, double deudaTotal)
        {

            PagoResponse pagoResponse = new PagoResponse();
            if(deudaTotal == 0 )
            {
                pagoResponse.doPago = false;
                pagoResponse.responseString = "No existe deuda, ya esta liquidada.";
                return pagoResponse;

            }
            else if (pagoValue > deudaTotal)
            {
                pagoResponse.doPago = false;
                pagoResponse.responseString = $"El valor del pago exede la deuda, debe ingresar un valor menor o igual al la deuda {deudaTotal}";
                return pagoResponse;

            }

          
            this.Value = pagoValue;
            this.fecha = new DateTime();

            pagoResponse.doPago = true;
            pagoResponse.responseString = $"Pago registrado con exito  {pagoValue}, que en saldo {deudaTotal - pagoValue} ";
            return pagoResponse;


        }
    }
}
