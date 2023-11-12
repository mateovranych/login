using Microsoft.EntityFrameworkCore;
using PrograTF3.Models;




namespace PrograTF3.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
