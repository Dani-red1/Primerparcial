using System;
using System.Collections.Generic;
using System.Linq;
namespace Primerparcial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Inventario sistema = new Inventario();
            sistema.Ejecutar();

        }
    }
    public class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public override string ToString()
        {
            return $"{Codigo}|{Nombre}|{Precio:F2}|{Cantidad}";
        }
    }

    
    public class Inventario
    {
        private List<Producto> productos = new List<Producto>();
        public void Ejecutar()
        {
            string opcion;

            do
            {
                Console.Clear();
                Menu();
                Console.Write("Seleccione la opcion deseada: ");
                opcion = Console.ReadLine()?.ToUpper() ?? "";

                switch (opcion)
                {
                    case "A":
                        AgregarProducto();
                        break;

                    case "B":
                        ListarProductos();
                        break;

                    case "C":
                        BuscarProductoCodigo();
                        break;

                    case "D":
                        MostrarProductoNoStock();
                        break;

                    case "E":
                        Console.WriteLine("\nSaliendo del sistema de inventario...");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida. Presiona cualquier tecla para seguir...");
                        Console.ReadKey();
                        break;

                }
                if (opcion != "E")
                {
                    Console.WriteLine("\n --- Fin ---");
                    Console.Write("Presiona cualquier tecla para ir al menu...");
                    Console.ReadKey();
                }
            } while (opcion != "E");
        }
        private void Menu()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine(" Inventario Rapido / ElectroPlus ");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("A. Agrega un producto");
            Console.WriteLine("B. Listar producto");
            Console.WriteLine("C. Buscar producto por codigo");
            Console.WriteLine("D. Mostrar productos con 0 stock");
            Console.WriteLine("E. Salir");
            Console.WriteLine("-");
        }
        private void AgregarProducto() //Solicita al usuario los datos del producto y los añade a la lista.
        {
            Console.WriteLine("\n--- Agregar un producto nuevo ---");
            Console.Write("Codigo: ");
            string codigo = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(codigo))
            {
                Console.WriteLine("El codigo no puede estar vacio.");
                return;
            }
            if (productos.Any(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Ya existe un producto con este codigo '{codigo}'.");
                return;
            }
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()?.Trim() ?? "";
            decimal precio = 0;
            int cantidad = 0;

            Console.Write("Precio (ej: 500,50): ");
            if (!ParseDecimal(Console.ReadLine(), "precio", out precio)) return;

            Console.Write("Cantidad (en stock): ");
            if (!ParseInt(Console.ReadLine(), "cantidad", out cantidad)) return;

            Producto NuevoProducto = new Producto
            {
                Codigo = codigo,
                Nombre = nombre,
                Precio = precio,
                Cantidad = cantidad
            };
            productos.Add(NuevoProducto);
            Console.WriteLine($"\n Producto '{nombre}' se agrego con exito");
        }
        private void ListarProductos() //Muestra todos los productos de la lista.
        {
            Console.WriteLine("\n--- Listado Completo de Productos ---");
            if (productos.Any())
            {
                Console.WriteLine("Formato: Código|Nombre|Precio|Cantidad");
                Console.WriteLine("-------------------------------------");
                foreach (var p in productos)
                {
                    Console.WriteLine(p.ToString());
                }
            }
            else
            {
                Console.WriteLine("No hay productos registrados en el inventario.");
            }
        }
        private void BuscarProductoCodigo() //Pide un codigo encuentra y muestra el producto especifico
        {
            Console.WriteLine("\n--- Buscar Producto por Código ---");
            Console.Write("Ingrese el código del producto a buscar: ");
            string CodigoBuscar = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(CodigoBuscar))
            {
                Console.WriteLine("El código de búsqueda no puede estar vacío.");
                return;
            }

            Producto ProductoEncontrado = productos.FirstOrDefault(p => p.Codigo.Equals(CodigoBuscar, StringComparison.OrdinalIgnoreCase));

            if (ProductoEncontrado != null)
            {
                Console.WriteLine("\nProducto Encontrado:");
                Console.WriteLine("Formato: Código|Nombre|Precio|Cantidad");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine(ProductoEncontrado.ToString());
            }
            else
            {
                Console.WriteLine($"\nProducto con código '{CodigoBuscar}' no se encontro.");
            }
        }
        private void MostrarProductoNoStock() //Usa filtra y muestra solo los productos de los cuales ya hay 0 o no hay stock.
        {
            Console.WriteLine("\n--- Productos con Stock Agotado (Cantidad = 0) ---");

            List<Producto> sinStock = productos.Where(p => p.Cantidad == 0).ToList();

            if (sinStock.Any())
            {
                Console.WriteLine("Formato: Código|Nombre|Precio|Cantidad");
                Console.WriteLine("-");
                foreach (var p in sinStock)
                {
                    Console.WriteLine(p.ToString());
                }
            }
            else
            {
                Console.WriteLine("Todos los productos tienen stock.");
            }
        }
        private bool ParseDecimal(string input, string campo, out decimal result)
        {
            result = 0;
            try
            {
                
                result = decimal.Parse(input);

                //la validacion ocurre solo si el parseo fue exitoso
                if (result < 0)
                {
                    Console.WriteLine($"Valor inválido: El {campo} no puede ser negativo. Intente de nuevo.");
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                //Captura el error de cuando el usuario pone texto en lugar de numero.
                Console.WriteLine($"Error de formato: El {campo} debe ser un valor numérico válido (ej. 10,50). Intente de nuevo.");
                return false;
            }
            catch (Exception ex)
            {
                //Captura algun error inesperado
                Console.WriteLine($"Ocurrió un error inesperado al leer el {campo}: {ex.Message}.");
                return false;
            }
        }

        private bool ParseInt(string input, string campo, out int result)
        {
            result = 0;
            try
            {
                //se intenta la conversión.
                result = int.Parse(input);

                //Validamos que no sea negativo
                if (result < 0)
                {
                    Console.WriteLine($"Valor inválido: La {campo} no puede ser negativa. Intente de nuevo.");
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                //Captura el error específico cuando el usuario ingresa texto o decimal en un entero.
                Console.WriteLine($"Error de formato: La {campo} debe ser un número entero válido. Intente de nuevo.");
                return false;
            }
            catch (Exception ex)
            {
                //Captura algun otro error inesperado.
                Console.WriteLine($"Ocurrió un error inesperado al leer la {campo}: {ex.Message}.");
                return false;
            }
        }
    }
}

