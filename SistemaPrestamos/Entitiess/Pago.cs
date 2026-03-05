using System;

namespace Entitiess
{
    public class Pago
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int NumeroCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal InteresPagado { get; set; }
        public decimal MontoAbonado { get; set; }
        public decimal NuevoSaldo { get; set; }
        public decimal CuotaMensual { get; set; }
        public bool Pagado { get; set; }
    }
}