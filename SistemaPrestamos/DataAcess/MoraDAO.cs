using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entitiess;

namespace DataAcess
{
    public class MoraDAO
    {
        public void Insertar(Mora m)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = @"INSERT INTO Moras (PrestamoId, PagoId, MontoMora, Fecha)
                               VALUES (@prestamoId, @pagoId, @monto, @fecha)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@prestamoId", m.PrestamoId);
                cmd.Parameters.AddWithValue("@pagoId", m.PagoId);
                cmd.Parameters.AddWithValue("@monto", m.MontoMora);
                cmd.Parameters.AddWithValue("@fecha", m.Fecha);
                cmd.ExecuteNonQuery();
            }
        }

        public int ContarMorasPorPrestamo(int prestamoId)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT COUNT(*) FROM Moras WHERE PrestamoId = @prestamoId";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@prestamoId", prestamoId);
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<Mora> ObtenerTodas()
        {
            var lista = new List<Mora>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Moras";
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Mora
                    {
                        Id = (int)reader["Id"],
                        PrestamoId = (int)reader["PrestamoId"],
                        PagoId = (int)reader["PagoId"],
                        MontoMora = (decimal)reader["MontoMora"],
                        Fecha = (DateTime)reader["Fecha"]
                    });
                }
            }
            return lista;
        }
    }
}