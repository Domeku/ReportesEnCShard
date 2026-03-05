using DataAcess;
using Entitiess;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLogic
{
    public class ReporteService
    {
        private PrestamoDAO prestamoDAO = new PrestamoDAO();
        private ClienteDAO clienteDAO = new ClienteDAO();
        private MoraDAO moraDAO = new MoraDAO();
        private PagoDAO pagoDAO = new PagoDAO();

        // Reporte #3: Total prestado y total intereses
        public (decimal totalPrestado, decimal totalIntereses) ObtenerReporteFinanciero()
        {
            var prestamos = prestamoDAO.ObtenerTodos();
            decimal totalPrestado = 0;
            decimal totalIntereses = 0;
            foreach (var p in prestamos)
            {
                totalPrestado += p.Monto;
                totalIntereses += p.InteresGenerado;
            }
            return (totalPrestado, totalIntereses);
        }

        // Reporte #4: Moras acumuladas por cliente
        public DataTable ObtenerReporteMoras()
        {
            var tabla = new DataTable();
            tabla.Columns.Add("Cliente");
            tabla.Columns.Add("Cantidad de Moras");

            var clientes = clienteDAO.ObtenerTodos();
            foreach (var c in clientes)
            {
                var prestamos = prestamoDAO.ObtenerPorCliente(c.Id);
                int totalMoras = 0;
                foreach (var p in prestamos)
                    totalMoras += moraDAO.ContarMorasPorPrestamo(p.Id);

                if (totalMoras > 0)
                    tabla.Rows.Add(c.NombreCompleto, totalMoras);
            }
            return tabla;
        }

        // Reporte #5: Lista de clientes morosos
        public List<Cliente> ObtenerClientesMorosos()
        {
            return clienteDAO.ObtenerMorosos();
        }
    }
}