using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using EjemplMVM.Commands;
using EjemplMVM.Modelo;
using EjemplMVM.Repositories;

namespace EjemplMVM.ViewModelo
{
    public class ProductoViewModel
    {
        public ObservableCollection<Producto> productos { set; get; } = new ObservableCollection<Producto>();
        public RelayCommand CargarProductosCommand { get; set; }
        public string textoBuscar { get; set; } = string.Empty;
        public ObservableCollection<Categoria> categorias { set; get; } = new ObservableCollection<Categoria>();
        public Categoria categoriaSeleccionada { get; set; }
        private IProductoRepository _repository;
        public ProductoViewModel()
        {
            _repository = new ProductoRepositoryImpl();
            CargarProductosCommand = new RelayCommand(BuscarProductos);

            CargarCategorias();

            CargarProductos();
        }

        private void BuscarProductos()
        {
            // Obtenemos el ID de la categoría (si es nulo, mandamos 0)
            int idCat = categoriaSeleccionada != null ? categoriaSeleccionada.Id : 0;

            // Usamos el nuevo método de búsqueda
            List<Producto> lista = _repository.BuscarPorFiltros(textoBuscar, idCat);

            productos.Clear();
            foreach (Producto producto in lista)
            {
                productos.Add(producto);
            }
        }

        private void CargarProductos()
        {
            List<Producto> lista = _repository.ObtenerTodos();
            productos.Clear();
            foreach (Producto producto in lista)
            {
                productos.Add(producto);
            }
        }
        private void CargarCategorias()
        {
            List<Categoria> lista = _repository.ObtenerCategorias();
            categorias.Clear();
            foreach (Categoria cat in lista)
            {
                categorias.Add(cat);
            }
            // Seleccionar por defecto la primera opción ("-- Todas --")
            if (categorias.Count > 0)
                categoriaSeleccionada = categorias[0];
        }
    }

}
