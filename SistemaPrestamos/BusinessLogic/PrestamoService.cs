using System;
using System.Collections.Generic;
using DataAcess;
using Entitiess;

namespace BusinessLogic
{
    public class PrestamoService
    {
        private PrestamoDAO prestamoDAO = new PrestamoDAO();
        private FondoBaseDAO fondoDAO = new FondoBaseDAO();

        public decimal ObtenerTasa(int plazoMeses)
        {
            if (plazoMeses >= 1 && plazoMeses <= 3) return 10m;
            if (plazoMeses >= 4 && plazoMeses <= 6) return 8m;
            if (plazoMeses >= 7 && plazoMeses <= 12) return 7m;
            return 5m;
        }

        public decimal CalcularCuota(decimal monto, decimal tasaAnual, int plazoMeses)
        {
            decimal tasaMensual = (tasaAnual / 100m) / 12m;
            if (tasaMensual == 0) return monto / plazoMeses;
            double i = (double)tasaMensual;
            double n = plazoMeses;
            double cuota = (double)monto * (i * Math.Pow(1 + i, n)) / (Math.Pow(1 + i, n) - 1);
            return Math.Round((decimal)cuota, 2);
        }

        public List<Pago> GenerarTablaAmortizacion(Prestamo p)
        {
            var tabla = new List<Pago>();
            decimal tasaMensual = (p.TasaInteres / 100m) / 12m;
            decimal saldo = p.Monto;

            for (int mes = 1; mes <= p.PlazoMeses; mes++)
            {
                decimal interes = Math.Round(saldo * tasaMensual, 2);
                decimal capital = Math.Round(p.CuotaMensual - interes, 2);
                decimal nuevoSaldo = Math.Round(saldo - capital, 2);
                if (mes == p.PlazoMeses) nuevoSaldo = 0;

                tabla.Add(new Pago
                {
                    PrestamoId = p.Id,
                    NumeroCuota = mes,
                    SaldoAnterior = saldo,
                    InteresPagado = interes,
                    MontoAbonado = capital,
                    NuevoSaldo = nuevoSaldo,
                    CuotaMensual = p.CuotaMensual,
                    Pagado = false
                });
                saldo = nuevoSaldo;
            }
            return tabla;
        }

        public string CrearPrestamo(Prestamo p, decimal sueldoCliente, string garantiaCliente)
        {
            if (p.Monto > sueldoCliente * 4) return "Error: Supera 4 veces el sueldo.";
            if (string.IsNullOrWhiteSpace(garantiaCliente)) return "Error: Falta garantía.";

            var fondo = fondoDAO.Obtener();
            if (fondo == null || fondo.Monto < p.Monto) return "Error: Fondos insuficientes.";

            p.TasaInteres = ObtenerTasa(p.PlazoMeses);
            p.CuotaMensual = CalcularCuota(p.Monto, p.TasaInteres, p.PlazoMeses);
            p.FechaInicio = DateTime.Today;

            prestamoDAO.Insertar(p);
            fondoDAO.Actualizar(fondo.Monto - p.Monto);
            return "OK";
        }

        public void AplicarAbonoExtraordinario(int prestamoId, decimal montoExtra)
        {
            var prestamo = prestamoDAO.ObtenerPorId(prestamoId);
            if (prestamo != null)
            {
                prestamo.Monto -= montoExtra;
                GenerarTablaAmortizacion(prestamo);
            }
        }
    }
}