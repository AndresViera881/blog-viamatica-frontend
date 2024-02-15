using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Viamatica.Blog.WASM.Pages
{
    public partial class RedireccionarAlAcceso
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [CascadingParameter]
        public Task<AuthenticationState>? estadoProveedorAutenticacion { get; set; }
        bool noAutorizado { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            var estadoAutorizacion = await estadoProveedorAutenticacion!;
            if (estadoAutorizacion.User == null)
            {
                var returnUrl = NavigationManager!.ToBaseRelativePath(NavigationManager.Uri);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    NavigationManager.NavigateTo("login", true);
                }
                else
                {
                    NavigationManager.NavigateTo($"login?returnUrl={returnUrl}");
                }
            }
            else
            {
                noAutorizado = true;
            }
        }
    }
}
