using System;

namespace Entitiess
{
    public class Mora
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int PagoId { get; set; }
        public decimal MontoMora { get; set; }
        public DateTime Fecha { get; set; }
    }
}