using System.Collections.Generic;
using System.Data.SqlClient;
using Entitiess;

namespace DataAcess
{
    public class ClienteDAO
    {
        public void Insertar(Cliente c)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = @"INSERT INTO Clientes 
                    (NombreCompleto, Correo, Telefono, Direccion, Garantia, Sueldo, EsMoroso)
                    VALUES (@nombre, @correo, @telefono, @direccion, @garantia, @sueldo, 0)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nombre", c.NombreCompleto);
                cmd.Parameters.AddWithValue("@correo", c.Correo);
                cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@garantia", c.Garantia);
                cmd.Parameters.AddWithValue("@sueldo", c.Sueldo);
                cmd.ExecuteNonQuery();
            }
        }

        public Cliente ObtenerPorId(int id)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Clientes WHERE Id = @id";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Cliente
                    {
                        Id = (int)reader["Id"],
                        NombreCompleto = reader["NombreCompleto"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Garantia = reader["Garantia"].ToString(),
                        Sueldo = (decimal)reader["Sueldo"],
                        EsMoroso = (bool)reader["EsMoroso"]
                    };
                }
                return null;
            }
        }

        public List<Cliente> ObtenerTodos()
        {
            var lista = new List<Cliente>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Clientes";
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = (int)reader["Id"],
                        NombreCompleto = reader["NombreCompleto"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Garantia = reader["Garantia"].ToString(),
                        Sueldo = (decimal)reader["Sueldo"],
                        EsMoroso = (bool)reader["EsMoroso"]
                    });
                }
            }
            return lista;
        }

        public void Actualizar(Cliente c)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = @"UPDATE Clientes SET 
                    NombreCompleto = @nombre,
                    Correo = @correo,
                    Telefono = @telefono,
                    Direccion = @direccion,
                    Garantia = @garantia,
                    Sueldo = @sueldo
                    WHERE Id = @id";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nombre", c.NombreCompleto);
                cmd.Parameters.AddWithValue("@correo", c.Correo);
                cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@garantia", c.Garantia);
                cmd.Parameters.AddWithValue("@sueldo", c.Sueldo);
                cmd.Parameters.AddWithValue("@id", c.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Cliente> ObtenerMorosos()
        {
            var lista = new List<Cliente>();
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "SELECT * FROM Clientes WHERE EsMoroso = 1";
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id = (int)reader["Id"],
                        NombreCompleto = reader["NombreCompleto"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Garantia = reader["Garantia"].ToString(),
                        Sueldo = (decimal)reader["Sueldo"],
                        EsMoroso = (bool)reader["EsMoroso"]
                    });
                }
            }
            return lista;
        }

        public void MarcarMoroso(int clienteId)
        {
            using (var con = Conexion.ObtenerConexion())
            {
                con.Open();
                string sql = "UPDATE Clientes SET EsMoroso = 1 WHERE Id = @id";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", clienteId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}