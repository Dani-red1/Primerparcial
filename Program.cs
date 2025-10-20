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
            sistema.ejecutar();

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
            Console.WriteLine("B. ");
            Console.WriteLine("C. ");
            Console.WriteLine("D. ");
            Console.WriteLine("E. ");
            Console.WriteLine("-");
        }

    }
}
