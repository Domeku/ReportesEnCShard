using System.Data.SqlClient;

namespace DataAcess
{
    public class Conexion
    {
        private static string cadena =
            "Server=.;Database=SistemaPrestamos;Integrated Security=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}