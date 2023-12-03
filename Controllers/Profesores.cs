using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PrograTF3.Models;

namespace PrograTF3.Controllers
{
    [Authorize(Policy = "RequiereAutenticacion")]

    public class Profesores : Controller
    {
        private readonly IConfiguration configuration;

        public Profesores(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = "SELECT * FROM profesores;";

            List<Profesore> listProfesores = new List<Profesore>();
            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                while(reader.Read())
                {
                    Profesore profesor = new Profesore();
                    profesor.Id = (int)reader["id"];
                    profesor.Nombre = reader["nombre"].ToString();
                    profesor.Apellido = reader["apellido"].ToString();

                    listProfesores.Add(profesor);


                }

                connection.Close();

            }

            return View(listProfesores);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = $"SELECT * FROM profesores WHERE id = {id};";

            Profesore profesor = new Profesore();
            
            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    profesor.Id = (int)reader["id"];
                    profesor.Nombre = reader["nombre"].ToString();
                    profesor.Apellido = reader["apellido"].ToString();
                }
                else
                {

                return NotFound();
                }

                connection.Close();

            }

            return View(profesor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Profesore profesor)
        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = $"UPDATE profesores SET nombre = '{profesor.Nombre}', apellido = '{profesor.Apellido}' WHERE id = {profesor.Id};" ;

            List<Profesore> listProfesores = new List<Profesore>();

            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                command.ExecuteNonQuery();

                query = "SELECT * FROM profesores;";

                MySqlCommand command2 = new MySqlCommand(query, connection);

                MySqlDataReader reader = command2.ExecuteReader();



                while (reader.Read())
                {
                    Profesore profesor2 = new Profesore();
                    profesor2.Id = (int)reader["id"];
                    profesor2.Nombre = reader["nombre"].ToString();
                    profesor2.Apellido = reader["apellido"].ToString();

                    listProfesores.Add(profesor2);

                }

                connection.Close();

            }
            return View("Index",listProfesores);
        }

    }
}

