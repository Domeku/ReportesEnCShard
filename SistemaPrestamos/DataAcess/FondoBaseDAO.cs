using System.Data.SqlClient;
using Entitiess;

namespace DataAcess
{
    public class FondoBaseDAO
    {
        public FondoBase Obtener()
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "SELECT TOP 1 * FROM FondoBase";
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new FondoBase { Id = (int)dr["Id"], Monto = (decimal)dr["Monto"] };
                        }
                    }
                }
            }
            return null;
        }

        public void Actualizar(decimal nuevoMonto)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "UPDATE FondoBase SET Monto = @monto";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@monto", nuevoMonto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}