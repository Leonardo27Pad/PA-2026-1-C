using System;
using System.Collections.Generic;
using System.Text;
using EjemplMVM.Modelo;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace EjemplMVM.Repositories
{
    public interface IProductoRepository
    {
        public List<Producto> ObtenerTodos();
        public List<Producto> BuscarPorNombre(string nombre);
        public List<Categoria> ObtenerCategorias();
        public List<Producto> BuscarPorFiltros(string nombre, int idCategoria);


    }
}
