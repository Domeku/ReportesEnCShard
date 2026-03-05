using System.Data.SqlClient;

namespace DataAcess
{
    public class Conexion
    {
        private string cadena = "Server=.;Database=SistemaPrestamos;Integrated Security=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}