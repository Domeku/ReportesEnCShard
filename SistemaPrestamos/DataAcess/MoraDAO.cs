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
                var query = "INSERT INTO Mora (PrestamoId, PagoId, MontoMora, Fecha) VALUES (@pid, @pagoid, @monto, @fecha)";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pid", m.PrestamoId);
                    cmd.Parameters.AddWithValue("@pagoid", m.PagoId);
                    cmd.Parameters.AddWithValue("@monto", m.MontoMora);
                    cmd.Parameters.AddWithValue("@fecha", m.Fecha);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ContarPorPrestamo(int prestamoId)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "SELECT COUNT(*) FROM Mora WHERE PrestamoId = @pid";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pid", prestamoId);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}