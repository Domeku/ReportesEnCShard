using System.Collections.Generic;
using DataAcess;
using Entitiess;

namespace BusinessLogic
{
    public class ClienteService
    {
        private ClienteDAO clienteDAO = new ClienteDAO();

        public string RegistrarCliente(Cliente c)
        {
            // Validar que tenga garantía
            if (string.IsNullOrWhiteSpace(c.Garantia))
                return "Error: El cliente debe tener una garantía.";

            if (string.IsNullOrWhiteSpace(c.NombreCompleto))
                return "Error: El nombre es obligatorio.";

            if (c.Sueldo <= 0)
                return "Error: El sueldo debe ser mayor a cero.";

            clienteDAO.Insertar(c);
            return "OK";
        }

        public string ActualizarCliente(Cliente c)
        {
            if (string.IsNullOrWhiteSpace(c.NombreCompleto))
                return "Error: El nombre es obligatorio.";

            if (c.Sueldo <= 0)
                return "Error: El sueldo debe ser mayor a cero.";

            clienteDAO.Actualizar(c);
            return "OK";
        }

        public Cliente ObtenerPorId(int id)
        {
            return clienteDAO.ObtenerPorId(id);
        }

        public List<Cliente> ObtenerTodos()
        {
            return clienteDAO.ObtenerTodos();
        }

        public List<Cliente> ObtenerMorosos()
        {
            return clienteDAO.ObtenerMorosos();
        }
    }
}