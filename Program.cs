

using System;
using System.Collections.Generic;

namespace AgendaPro
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public Persona(int id, string nombre, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
        }

        public override string ToString()
        {
            return $"{Id} | {Nombre} | {Telefono}";
        }
    }

    public class Cita
    {
        public int PersonaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        public Cita(int personaId, DateTime fecha, string descripcion)
        {
            PersonaId = personaId;
            Fecha = fecha;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"{PersonaId} | {Fecha:yyyy-MM-dd HH:mm} | {Descripcion}";
        }
    }

    class Program
    {
        static List<Persona> personas = new List<Persona>();
        static List<Cita> citas = new List<Cita>();

        static void Main(string[] args)
        {
            Console.WriteLine("=== AgendaPro - Gestión de Personas y Citas ===\n");

            // Agregar varios nombres y teléfonos iniciales
            personas.Add(new Persona(1, "Ana Ramírez", "8888-1234"));
            personas.Add(new Persona(2, "Carlos Soto", "8877-5678"));
            personas.Add(new Persona(3, "María López", "8833-9012"));
            personas.Add(new Persona(4, "Jorge Méndez", "8899-4567"));
            personas.Add(new Persona(5, "Sofía Vargas", "8822-7890"));

            bool salir = false;
            while (!salir)
            {
                MostrarMenu();
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": RegistrarPersona(); break;
                    case "2": ListarPersonas(); break;
                    case "3": CrearCita(); break;
                    case "4": ListarCitasPorPersona(); break;
                    case "5": MostrarTodasLasCitas(); break;
                    case "6": salir = true; break;
                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.\n");
                        break;
                }
            }

            Console.WriteLine("Saliendo... Gracias por usar AgendaPro.");
        }

        static void MostrarMenu()
        {
            Console.WriteLine("Menú:");
            Console.WriteLine("1. Registrar persona (validar Id único)");
            Console.WriteLine("2. Listar personas");
            Console.WriteLine("3. Crear cita para una persona (validar PersonaId)");
            Console.WriteLine("4. Listar citas por PersonaId");
            Console.WriteLine("5. Mostrar todas las citas (PersonaId|Fecha|Descripcion)");
            Console.WriteLine("6. Salir\n");
        }

        static void RegistrarPersona()
        {
            try
            {
                Console.Write("Ingrese Id (entero): ");
                int id = int.Parse(Console.ReadLine());

                foreach (var p in personas)
                {
                    if (p.Id == id)
                    {
                        Console.WriteLine("Ya existe una persona con ese Id. Registro cancelado.\n");
                        return;
                    }
                }

                Console.Write("Ingrese Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Ingrese Teléfono: ");
                string telefono = Console.ReadLine();

                Persona nueva = new Persona(id, nombre, telefono);
                personas.Add(nueva);

                Console.WriteLine("Persona registrada correctamente.\n");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: El Id debe ser un número entero. Intentá de nuevo.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}\n");
            }
        }

        static void ListarPersonas()
        {
            if (personas.Count == 0)
            {
                Console.WriteLine("No hay personas registradas.\n");
                return;
            }

            Console.WriteLine("Lista de personas:");
            foreach (var p in personas)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine();
        }

        static void CrearCita()
        {
            try
            {
                Console.Write("Ingrese PersonaId para asignar la cita: ");
                int pid = int.Parse(Console.ReadLine());

                Persona personaEncontrada = null;
                foreach (var p in personas)
                {
                    if (p.Id == pid)
                    {
                        personaEncontrada = p;
                        break;
                    }
                }

                if (personaEncontrada == null)
                {
                    Console.WriteLine("No existe una persona con ese Id. Cree la persona primero.\n");
                    return;
                }

                Console.Write("Ingrese Fecha y hora (ej: 2025-12-31 14:30): ");
                string fechaTexto = Console.ReadLine();

                DateTime fecha;
                try
                {
                    fecha = DateTime.Parse(fechaTexto);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato de fecha inválido. Use YYYY-MM-DD HH:MM. Cita no creada.\n");
                    return;
                }

                Console.Write("Ingrese Descripción de la cita: ");
                string descripcion = Console.ReadLine();

                Cita nuevaCita = new Cita(pid, fecha, descripcion);
                citas.Add(nuevaCita);

                Console.WriteLine($"Cita creada para {personaEncontrada.Nombre} el {fecha:yyyy-MM-dd HH:mm}.\n");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: El Id debe ser un número entero. Intentá de nuevo.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}\n");
            }
        }

        static void ListarCitasPorPersona()
        {
            try
            {
                Console.Write("Ingrese PersonaId para listar sus citas: ");
                int pid = int.Parse(Console.ReadLine());

                bool encontro = false;
                foreach (var c in citas)
                {
                    if (c.PersonaId == pid)
                    {
                        Console.WriteLine(c.ToString());
                        encontro = true;
                    }
                }

                if (!encontro)
                {
                    Console.WriteLine("No se encontraron citas para ese PersonaId.\n");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: El Id debe ser un número entero. Intentá de nuevo.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}\n");
            }
        }

        static void MostrarTodasLasCitas()
        {
            if (citas.Count == 0)
            {
                Console.WriteLine("No hay citas registradas.\n");
                return;
            }

            Console.WriteLine("Todas las citas (PersonaId|Fecha|Descripcion):");
            foreach (var c in citas)
            {
                Console.WriteLine(c.ToString());
            }
            Console.WriteLine();
        }
    }
}
