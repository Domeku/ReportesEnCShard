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
        private PrestamoDAO prestamoDAO = new PrestamoDAO();

        public string RegistrarPago(int pagoId)
        {
            Pago pago = pagoDAO.ObtenerPorId(pagoId);
            if (pago == null || pago.Pagado) return "Error en pago.";
            pago.Pagado = true;
            pago.FechaPago = DateTime.Today;
            pagoDAO.Actualizar(pago);
            return "OK";
        }

        public string RegistrarMora(int pagoId, int prestamoId)
        {
            Pago pago = pagoDAO.ObtenerPorId(pagoId);
            if (pago == null || pago.Pagado) return "No aplica mora.";

            decimal montoMora = Math.Round(pago.CuotaMensual * 0.10m, 2);
            moraDAO.Insertar(new Mora
            {
                PrestamoId = prestamoId,
                PagoId = pagoId,
                MontoMora = montoMora,
                Fecha = DateTime.Today
            });

            int totalMoras = moraDAO.ContarPorPrestamo(prestamoId);
            if (totalMoras >= 3)
            {
                var prestamo = prestamoDAO.ObtenerPorId(prestamoId);
                if (prestamo != null) clienteDAO.MarcarMoroso(prestamo.ClienteId);
            }
            return "OK";
        }
    }
}