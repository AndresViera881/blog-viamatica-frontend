using Microsoft.AspNetCore.Components;
using Viamatica.Blog.WASM.Modelos;
using Viamatica.Blog.WASM.Servicios.IServicios;

namespace Viamatica.Blog.WASM.Pages.Autenticacion
{
    public partial class Registro
    {
        private UsuarioRegistro UsuarioRegistro = new UsuarioRegistro();

        public bool EstadoProcesando { get; set; } = false;
        public bool MostrarErroresRegistro { get; set; } = false;
        public IEnumerable<string> Errores { get; set; } = new List<string>();

        [Inject]
        public IAutenticacionService? _serviceAutenticacion { get; set; }
        [Inject]
        public NavigationManager? _navigationManager { get; set; }

        private async Task RegistroUsuario()
        {
            EstadoProcesando = true;
            MostrarErroresRegistro = false;
            var resultado = await _serviceAutenticacion!.Registro(UsuarioRegistro);
            if (resultado.RegistroCorrecto)
            {
                EstadoProcesando = false;
                _navigationManager!.NavigateTo("/login");
            }
            else
            {
                EstadoProcesando = false;
                MostrarErroresRegistro = true;
                Errores = resultado.Errores;
            }
        }
    }
}
