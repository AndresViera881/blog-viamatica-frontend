using Microsoft.AspNetCore.Components;
using Viamatica.Blog.WASM.Servicios.IServicios;

namespace Viamatica.Blog.WASM.Pages.Autenticacion
{
    public partial class Salir
    {
        [Inject]
        public IAutenticacionService _servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
           await _servicioAutenticacion.Salir();
            _navigationManager.NavigateTo("/");
        }
    }
}
