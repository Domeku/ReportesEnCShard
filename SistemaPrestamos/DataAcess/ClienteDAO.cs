using Entitiess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAcess
{
    public class ClienteDAO
    {
        Conexion con = new Conexion();

        // Guardar cliente nuevo
        public void Guardar(Cliente c)
        {
            using (SqlConnection conn = con.ObtenerConexion())
            {
                conn.Open();
                string sql = @"INSERT INTO Clientes 
                    (NombreCompleto, Correo, Telefono, Direccion, Garantia, Sueldo, EsMoroso)
                    VALUES (@nombre, @correo, @telefono, @direccion, @garantia, @sueldo, 0)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nombre", c.NombreCompleto);
                cmd.Parameters.AddWithValue("@correo", c.Correo);
                cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@garantia", c.Garantia);
                cmd.Parameters.AddWithValue("@sueldo", c.Sueldo);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener todos los clientes
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conn = con.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT * FROM Clientes";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

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

        // Obtener cliente por ID
        public Cliente ObtenerPorId(int id)
        {
            Cliente c = null;

            using (SqlConnection conn = con.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT * FROM Clientes WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    c = new Cliente
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
            }
            return c;
        }

        // Actualizar información personal del cliente
        public void Actualizar(Cliente c)
        {
            using (SqlConnection conn = con.ObtenerConexion())
            {
                conn.Open();
                string sql = @"UPDATE Clientes SET 
                    NombreCompleto = @nombre,
                    Correo = @correo,
                    Telefono = @telefono,
                    Direccion = @direccion,
                    Garantia = @garantia
                    WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nombre", c.NombreCompleto);
                cmd.Parameters.AddWithValue("@correo", c.Correo);
                cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@garantia", c.Garantia);
                cmd.Parameters.AddWithValue("@id", c.Id);
                cmd.ExecuteNonQuery();
            }
        }

        // Marcar cliente como moroso
        public void MarcarMoroso(int clienteId)
        {
            using (SqlConnection conn = con.ObtenerConexion())
            {
                conn.Open();
                string sql = "UPDATE Clientes SET EsMoroso = 1 WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", clienteId);
                cmd.ExecuteNonQuery();
            }
        }

        // Obtener clientes morosos
        public List<Cliente> ObtenerMorosos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conn = con.ObtenerConexion())
            {
                conn.Open();
                string sql = "SELECT * FROM Clientes WHERE EsMoroso = 1";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

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
    }
}