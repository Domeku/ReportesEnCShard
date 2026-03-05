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
                string sql = "SELECT TOP 1 * FROM FondoBase";
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FondoBase
                    {
                        Id = (int)reader["Id"],
                        Monto = (decimal)reader["Monto"]
                    };
                }
                return null;
            }
        }

        public void Actualizar(decimal nuevoMonto)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "UPDATE FondoBase SET Monto = @monto";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@monto", nuevoMonto);
                cmd.ExecuteNonQuery();
            }
        }
    }
}