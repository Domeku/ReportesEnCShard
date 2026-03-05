using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entitiess;

namespace DataAcess
{
    public class PagoDAO
    {
        public void Insertar(Pago p)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = @"INSERT INTO Pagos 
                    (PrestamoId, NumeroCuota, SaldoAnterior, InteresPagado, MontoAbonado, NuevoSaldo, CuotaMensual, Pagado)
                    VALUES (@prestamoId, @numeroCuota, @saldoAnterior, @interesPagado, @montoAbonado, @nuevoSaldo, @cuotaMensual, 0)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@prestamoId", p.PrestamoId);
                cmd.Parameters.AddWithValue("@numeroCuota", p.NumeroCuota);
                cmd.Parameters.AddWithValue("@saldoAnterior", p.SaldoAnterior);
                cmd.Parameters.AddWithValue("@interesPagado", p.InteresPagado);
                cmd.Parameters.AddWithValue("@montoAbonado", p.MontoAbonado);
                cmd.Parameters.AddWithValue("@nuevoSaldo", p.NuevoSaldo);
                cmd.Parameters.AddWithValue("@cuotaMensual", p.CuotaMensual);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Pago> ObtenerPorPrestamo(int prestamoId)
        {
            var lista = new List<Pago>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Pagos WHERE PrestamoId = @prestamoId ORDER BY NumeroCuota";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@prestamoId", prestamoId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Pago
                    {
                        Id = (int)reader["Id"],
                        PrestamoId = (int)reader["PrestamoId"],
                        NumeroCuota = (int)reader["NumeroCuota"],
                        SaldoAnterior = (decimal)reader["SaldoAnterior"],
                        InteresPagado = (decimal)reader["InteresPagado"],
                        MontoAbonado = (decimal)reader["MontoAbonado"],
                        NuevoSaldo = (decimal)reader["NuevoSaldo"],
                        CuotaMensual = (decimal)reader["CuotaMensual"],
                        Pagado = (bool)reader["Pagado"]
                    });
                }
            }
            return lista;
        }

        public void MarcarPagado(int pagoId, DateTime fechaPago)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "UPDATE Pagos SET Pagado = 1, FechaPago = @fecha WHERE Id = @id";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@fecha", fechaPago);
                cmd.Parameters.AddWithValue("@id", pagoId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}