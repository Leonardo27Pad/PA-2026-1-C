using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using EjemplMVM.Modelo;
using Microsoft.Data.SqlClient;

namespace EjemplMVM.Repositories
{
    public class ProductoRepositoryImpl : IProductoRepository
    {
        string cn = string.Empty;
        public ProductoRepositoryImpl()
        {
            cn = ConfigurationManager.ConnectionStrings["EjemplMVM.Properties.Settings.NorthwindDB"].ConnectionString;
        }
        public List<Producto> BuscarPorNombre(string nombre)
        {
            using (SqlConnection conex = new SqlConnection(cn))
            {
                conex.Open();
                string query = "SELECT ProductID,ProductName,UnitPrice,Discontinued From Products WHERE (@Nombre IS NULL OR ProductName LIKE @Nombre)";
                SqlCommand command = new SqlCommand(query, conex);

                object nombreParametro = string.IsNullOrEmpty(nombre) ? DBNull.Value : "%" + nombre + "%";
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = nombreParametro;

                SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                List<Producto> listaProductos = new List<Producto>();
                while (reader.Read())
                {
                    Producto producto = new Producto
                    {
                        Id = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        precio = reader.GetDecimal(2),
                        discontinuado = reader.GetBoolean(3)
                    };
                    listaProductos.Add(producto);
                }
                return listaProductos;
            }
        }

        public List<Producto> ObtenerTodos()
        {


            using (SqlConnection conex = new SqlConnection(cn))
            {
                conex.Open();
                string query = "SELECT ProductID,ProductName,UnitPrice,Discontinued From Products";

                SqlCommand command = new SqlCommand(query, conex);

                SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                List<Producto> listaProductos = new List<Producto>();
                while (reader.Read())
                {
                    Producto producto = new Producto
                    {
                        Id = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        precio = reader.GetDecimal(2),
                        discontinuado = reader.GetBoolean(3)
                    };
                    listaProductos.Add(producto);
                }
                return listaProductos;
            }
        }
        public List<Categoria> ObtenerCategorias()
        {
            using (SqlConnection conex = new SqlConnection(cn))
            {
                conex.Open();
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlCommand command = new SqlCommand(query, conex);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                List<Categoria> lista = new List<Categoria>();
                // Añadimos una categoría "falsa" para representar la opción "Todas"
                lista.Add(new Categoria { Id = 0, Nombre = "-- Todas --" });

                while (reader.Read())
                {
                    lista.Add(new Categoria
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1)
                    });
                }
                return lista;
            }
        }

        public List<Producto> BuscarPorFiltros(string nombre, int idCategoria)
        {
            using (SqlConnection conex = new SqlConnection(cn))
            {
                conex.Open();
                // Si @IdCategoria es 0, ignora el filtro de categoría. Si no, busca por ID.
                string query = "SELECT ProductID, ProductName, UnitPrice, Discontinued FROM Products WHERE (@Nombre IS NULL OR ProductName LIKE @Nombre) AND (@IdCategoria = 0 OR CategoryID = @IdCategoria)";
                SqlCommand command = new SqlCommand(query, conex);

                object nombreParametro = string.IsNullOrEmpty(nombre) ? DBNull.Value : "%" + nombre + "%";
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = nombreParametro;
                command.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategoria;

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                List<Producto> listaProductos = new List<Producto>();
                while (reader.Read())
                {
                    listaProductos.Add(new Producto
                    {
                        Id = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        precio = reader.GetDecimal(2),
                        discontinuado = reader.GetBoolean(3)
                    });
                }
                return listaProductos;
            }
        }
    }
}
