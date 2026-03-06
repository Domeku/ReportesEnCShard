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
                var query = @"INSERT INTO Prestamo (ClienteId, Monto, PlazoMeses, TasaInteres, CuotaMensual, FechaInicio, Estado) 
                              VALUES (@cid, @monto, @plazo, @tasa, @cuota, @fecha, 'Activo')";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@cid", p.ClienteId);
                    cmd.Parameters.AddWithValue("@monto", p.Monto);
                    cmd.Parameters.AddWithValue("@plazo", p.PlazoMeses);
                    cmd.Parameters.AddWithValue("@tasa", p.TasaInteres);
                    cmd.Parameters.AddWithValue("@cuota", p.CuotaMensual);
                    cmd.Parameters.AddWithValue("@fecha", p.FechaInicio);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ESTE ES EL MÉTODO QUE FALTA Y CAUSA EL ERROR
        public Prestamo ObtenerPorId(int id)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "SELECT * FROM Prestamo WHERE Id = @id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Prestamo
                            {
                                Id = (int)dr["Id"],
                                ClienteId = (int)dr["ClienteId"],
                                Monto = (decimal)dr["Monto"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Prestamo> ObtenerTodos()
        {
            var lista = new List<Prestamo>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "SELECT * FROM Prestamo";
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Prestamo
                            {
                                Id = (int)dr["Id"],
                                ClienteId = (int)dr["ClienteId"],
                                Monto = (decimal)dr["Monto"]
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Prestamo> ObtenerPorCliente(int clienteId)
        {
            var lista = new List<Prestamo>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "SELECT * FROM Prestamo WHERE ClienteId = @cid";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@cid", clienteId);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Prestamo
                            {
                                Id = (int)dr["Id"],
                                ClienteId = (int)dr["ClienteId"],
                                Monto = (decimal)dr["Monto"]
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}