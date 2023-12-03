using Microsoft.AspNetCore.Mvc;
using PrograTF3.Models;
using PrograTF3.Recursos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MySql.Data.MySqlClient;

namespace PrograTF3.Controllers
{
    public class InicioController : Controller
    {
        private readonly IConfiguration configuration;
        public InicioController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Registrarse()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            modelo.Clave = Utilidades.EncriptarClave(modelo.Clave);

            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string query = $"INSERT INTO usuarios (nombreusuario, correo, clave) VALUES ('{modelo.NombreUsuario}','{modelo.Correo}','{modelo.Clave}');";

            int resultado = 0;
            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                
                resultado = command.ExecuteNonQuery();

                connection.Close(); 
            }


            if (resultado > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";

            return View();
        }


        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)

        {
            string cadenaConexion = configuration.GetConnectionString("cadenaSQL");

            string claveEncriptada = Utilidades.EncriptarClave(clave);

            string query = $"SELECT * FROM usuarios WHERE correo = '{correo}' AND clave = '{claveEncriptada}';";


            using (MySqlConnection connection = new MySqlConnection(cadenaConexion))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    string nombreUsuario = reader["nombreUsuario"].ToString()!;

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, nombreUsuario)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true

                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties


                        );
                    connection.Close();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    connection.Close();
                    ViewData["Mensaje"] = "No se encontraron coincidencias";
                    return View();
                }



            }
        }
    }
}
