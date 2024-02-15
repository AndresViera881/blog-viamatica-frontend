using Microsoft.AspNetCore.Components;
using System.Web;
using Viamatica.Blog.WASM.Modelos;
using Viamatica.Blog.WASM.Servicios.IServicios;

namespace Viamatica.Blog.WASM.Pages.Autenticacion
{
    public partial class Login
    {
        private UsuarioAutenticacion UsuarioAutenticacion = new UsuarioAutenticacion();

        public bool EstadoProcesando { get; set; } = false;
        public bool MostrarErroresLogin { get; set; } = false;
        public string? Errores { get; set; }
        public string UrlRetorno { get; set; } = string.Empty;

        [Inject]
        public IAutenticacionService? _serviceAutenticacion { get; set; }
        [Inject]
        public NavigationManager? _navigationManager { get; set; }

        private async Task IniciarSesion()
        {
            EstadoProcesando = true;
            MostrarErroresLogin = false;
            var resultado = await _serviceAutenticacion!.Login(UsuarioAutenticacion);
            if (resultado.isSuccess)
            {
                var url = new Uri(_navigationManager.Uri);
                var parametrosQuery = HttpUtility.ParseQueryString(url.Query);
                UrlRetorno = parametrosQuery["returnUrl"];
                if (string.IsNullOrEmpty(UrlRetorno))
                {
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    _navigationManager.NavigateTo("/" + UrlRetorno);
                }
                EstadoProcesando = false;
                _navigationManager!.NavigateTo("/publicaciones");
            }
            else 
            {
                EstadoProcesando = false;
                MostrarErroresLogin = true;
                Errores = "Usuario o contraseña incorrectos.";
                _navigationManager!.NavigateTo("/login");
            }
        }

    }
}
