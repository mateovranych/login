using Microsoft.EntityFrameworkCore;
using TrabajoFinalProgramacion.Models;




namespace TrabajoFinalProgramacion.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
