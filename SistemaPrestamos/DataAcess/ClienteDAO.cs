using System;
using System.Data.SqlClient;
using Entitiess;

namespace DataAcess
{
    public class ClienteDAO
    {
        // Este es el método que ya tenía la Persona 1
        public void Insertar(Cliente c)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "INSERT INTO Cliente (NombreCompleto, Correo, Telefono, Direccion, Garantia, Sueldo, EsMoroso) VALUES (@nom, @cor, @tel, @dir, @gar, @suel, @mor)";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nom", c.NombreCompleto);
                    cmd.Parameters.AddWithValue("@cor", c.Correo);
                    cmd.Parameters.AddWithValue("@tel", c.Telefono);
                    cmd.Parameters.AddWithValue("@dir", c.Direccion);
                    cmd.Parameters.AddWithValue("@gar", c.Garantia);
                    cmd.Parameters.AddWithValue("@suel", c.Sueldo);
                    cmd.Parameters.AddWithValue("@mor", c.EsMoroso);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Este es el método que agregamos nosotros
        public void MarcarMoroso(int clienteId)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                var query = "UPDATE Cliente SET EsMoroso = 1 WHERE Id = @id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", clienteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}