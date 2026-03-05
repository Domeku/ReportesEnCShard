using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entitiess;

namespace DataAcess
{
    public class PrestamoDAO
    {
        public void Insertar(Prestamo p)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = @"INSERT INTO Prestamos 
                    (ClienteId, Monto, PlazoMeses, TasaInteres, InteresGenerado, MontoTotal, CuotaMensual, FechaInicio, Estado)
                    VALUES (@clienteId, @monto, @plazo, @tasa, @interes, @total, @cuota, @fecha, 'Activo')";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@clienteId", p.ClienteId);
                cmd.Parameters.AddWithValue("@monto", p.Monto);
                cmd.Parameters.AddWithValue("@plazo", p.PlazoMeses);
                cmd.Parameters.AddWithValue("@tasa", p.TasaInteres);
                cmd.Parameters.AddWithValue("@interes", p.InteresGenerado);
                cmd.Parameters.AddWithValue("@total", p.MontoTotal);
                cmd.Parameters.AddWithValue("@cuota", p.CuotaMensual);
                cmd.Parameters.AddWithValue("@fecha", p.FechaInicio);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Prestamo> ObtenerPorCliente(int clienteId)
        {
            var lista = new List<Prestamo>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Prestamos WHERE ClienteId = @clienteId";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@clienteId", clienteId);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Prestamo
                    {
                        Id = (int)reader["Id"],
                        ClienteId = (int)reader["ClienteId"],
                        Monto = (decimal)reader["Monto"],
                        PlazoMeses = (int)reader["PlazoMeses"],
                        TasaInteres = (decimal)reader["TasaInteres"],
                        InteresGenerado = (decimal)reader["InteresGenerado"],
                        MontoTotal = (decimal)reader["MontoTotal"],
                        CuotaMensual = (decimal)reader["CuotaMensual"],
                        FechaInicio = (DateTime)reader["FechaInicio"],
                        Estado = reader["Estado"].ToString()
                    });
                }
            }
            return lista;
        }

        public List<Prestamo> ObtenerTodos()
        {
            var lista = new List<Prestamo>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Prestamos";
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Prestamo
                    {
                        Id = (int)reader["Id"],
                        ClienteId = (int)reader["ClienteId"],
                        Monto = (decimal)reader["Monto"],
                        PlazoMeses = (int)reader["PlazoMeses"],
                        TasaInteres = (decimal)reader["TasaInteres"],
                        InteresGenerado = (decimal)reader["InteresGenerado"],
                        MontoTotal = (decimal)reader["MontoTotal"],
                        CuotaMensual = (decimal)reader["CuotaMensual"],
                        FechaInicio = (DateTime)reader["FechaInicio"],
                        Estado = reader["Estado"].ToString()
                    });
                }
            }
            return lista;
        }
    }
}