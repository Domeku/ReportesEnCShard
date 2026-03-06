using System;
using DataAcess;
using Entitiess;

namespace BusinessLogic
{
    public class PagoService
    {
        private PagoDAO pagoDAO = new PagoDAO();
        private MoraDAO moraDAO = new MoraDAO();
        private ClienteDAO clienteDAO = new ClienteDAO();

        // Registra el pago de una cuota
        public string RegistrarPago(int pagoId)
        {
            Pago pago = pagoDAO.ObtenerPorId(pagoId);
            if (pago == null)
                return "Error: La cuota no existe.";

            if (pago.Pagado)
                return "Error: Esta cuota ya fue pagada.";

            pago.Pagado = true;
            pago.FechaPago = DateTime.Today;
            pagoDAO.Actualizar(pago);

            return "OK";
        }

        // Registra una mora si la cuota no fue pagada ese mes
        public string RegistrarMora(int pagoId, int prestamoId)
        {
            Pago pago = pagoDAO.ObtenerPorId(pagoId);
            if (pago == null)
                return "Error: La cuota no existe.";

            if (pago.Pagado)
                return "Error: La cuota ya fue pagada, no aplica mora.";

            // Mora = 10% de la cuota mensual
            decimal montoMora = Math.Round(pago.CuotaMensual * 0.10m, 2);

            var mora = new Mora
            {
                PrestamoId = prestamoId,
                PagoId = pagoId,
                MontoMora = montoMora,
                Fecha = DateTime.Today
            };

            moraDAO.Insertar(mora);

            // Verificar si acumuló 3 moras → marcar como moroso
            int totalMoras = moraDAO.ContarPorPrestamo(prestamoId);
            if (totalMoras >= 3)
            {
                Prestamo prestamo = new PrestamoDAO().ObtenerPorId(prestamoId);
                if (prestamo != null)
                    clienteDAO.MarcarMoroso(prestamo.ClienteId);
            }

            return "OK";
        }
    }
}
