using System;
using System.Data.SqlClient;
using Entitiess;

namespace DataAcess
{
    public class PagoDAO
    {
        public Pago ObtenerPorId(int id)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "SELECT * FROM Pago WHERE Id = @id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Pago
                            {
                                Id = (int)dr["Id"],
                                PrestamoId = (int)dr["PrestamoId"],
                                NumeroCuota = (int)dr["NumeroCuota"],
                                Pagado = (bool)dr["Pagado"],
                                CuotaMensual = (decimal)dr["CuotaMensual"],
                                FechaPago = dr["FechaPago"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["FechaPago"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Actualizar(Pago p)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "UPDATE Pago SET Pagado = @pagado, FechaPago = @fecha WHERE Id = @id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pagado", p.Pagado);
                    cmd.Parameters.AddWithValue("@fecha", (object)p.FechaPago ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", p.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}