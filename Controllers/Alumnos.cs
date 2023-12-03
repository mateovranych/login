using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrograTF3.Models;
using MySql.Data.MySqlClient;

namespace PrograTF3.Controllers
{
    [Authorize(Policy = "RequiereAutenticacion")]
    public class Alumnos : Controller
    {
        private readonly IConfiguration configuration;

        public Alumnos(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task<IActionResult> Index()
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = "SELECT * FROM alumnos LIMIT 1;";

            Alumno alumno = new Alumno();
            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    alumno.Nombre = reader["nombre"].ToString();
                    alumno.Dni = (int)reader["dni"] ;
                    
                    connection.Close();

                }

                TempData["indice"] = 0;
                return View(alumno);

            }

        }

        public async Task<IActionResult> Next()
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = $"SELECT * FROM alumnos";

            List<Alumno> listAlumnos = new List<Alumno>();

            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                while(reader.Read())
                {

                    Alumno alumno = new Alumno();

                    alumno.Nombre = reader["nombre"].ToString()!;

                    alumno.Dni = (int)reader["dni"];

                    listAlumnos.Add(alumno);
                }

                connection.Close();


            }

                ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                var indice = (int)TempData["indice"]! + 1;

                if (indice > listAlumnos.Count - 1)
                {
                    indice = listAlumnos.Count - 1;
                }

                TempData["sesion"] = true;
                TempData["indice"] = indice;
                return View("Index", listAlumnos[indice]);

        }
        public async Task<IActionResult> Previous()
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = $"SELECT * FROM alumnos";

            List<Alumno> listAlumnos = new List<Alumno>();

            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {

                    Alumno alumno = new Alumno();

                    alumno.Nombre = reader["nombre"].ToString()!;

                    alumno.Dni = (int)reader["dni"];

                    listAlumnos.Add(alumno);
                }

                connection.Close();


            }


            ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                var indice = (int)TempData["indice"]! - 1;

                if (indice < 0)
                {
                    indice = 0;
                }

                TempData["sesion"] = true;
                TempData["indice"] = indice;
                return View("Index", listAlumnos[indice]);

            
         

        }
        public async Task<IActionResult> FirstStudent()
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = $"SELECT * FROM alumnos LIMIT 1;";

            Alumno alumno = new Alumno();
            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    alumno.Nombre = reader["nombre"].ToString();
                    alumno.Dni = (int)reader["dni"];

                    connection.Close();

                }

                TempData["indice"] = 0;

            }

                ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                TempData["sesion"] = true;
                TempData["indice"] = 0;
                return View("Index", alumno);


        }
        public async Task<IActionResult> LastStudent()
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = $"SELECT * FROM alumnos";

            List<Alumno> listAlumnos = new List<Alumno>();

            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {

                    Alumno alumno = new Alumno();

                    alumno.Nombre = reader["nombre"].ToString()!;

                    alumno.Dni = (int)reader["dni"];

                    listAlumnos.Add(alumno);
                }

                connection.Close();


            }


                ViewBag.nombre = TempData["nombreUsuario"];
                ViewBag.inicioSesion = true;
                ViewBag.session = true;

                TempData["sesion"] = true;
                TempData["indice"] = listAlumnos.Count - 1;
                return View("Index", listAlumnos.Last());

        

        }

    }
}
