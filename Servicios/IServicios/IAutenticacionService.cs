using Viamatica.Blog.WASM.Modelos;

namespace Viamatica.Blog.WASM.Servicios.IServicios
{
    public interface IAutenticacionService
    {
        
        Task<RespuestaRegistro> Registro(UsuarioRegistro usuarioRegistro);

        Task<RespuestaAutenticacion> Login(UsuarioAutenticacion usuarioAutenticacion);
        Task Salir();
    }
}
