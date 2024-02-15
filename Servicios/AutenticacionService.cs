using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using Viamatica.Blog.WASM.Helper;
using Viamatica.Blog.WASM.Modelos;
using Viamatica.Blog.WASM.Servicios.IServicios;

namespace Viamatica.Blog.WASM.Servicios
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AutenticacionService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<RespuestaAutenticacion> Login(UsuarioAutenticacion usuarioAutenticacion)
        {
            var content = JsonConvert.SerializeObject(usuarioAutenticacion);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync($"{InicializarUrl.UrlBaseApi}api/Usuario/Login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = (JObject)JsonConvert.DeserializeObject(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                var token = resultado["result"]["token"].Value<string>();
                var usuario = resultado["result"]["usuario"]["nombre"].Value<string>();
                await _localStorage.SetItemAsync(InicializarUrl.Token, token);
                await _localStorage.SetItemAsync(InicializarUrl.Usuario, usuario);
                ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(token);
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return new RespuestaAutenticacion { isSuccess = true };
            }
            else 
            {
                return new RespuestaAutenticacion { isSuccess = false };
            }
            
        }

        public async Task<RespuestaRegistro> Registro(UsuarioRegistro usuarioRegistro)
        {
            var content = JsonConvert.SerializeObject(usuarioRegistro);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync($"{InicializarUrl.UrlBaseApi}api/Usuario/RegistroUsuario", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<RespuestaRegistro>(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                return new RespuestaRegistro { RegistroCorrecto = true };
            }
            else
            {
                return resultado;
            }

        }

        public async Task Salir()
        {
            await _localStorage.RemoveItemAsync(InicializarUrl.Token);
            await _localStorage.RemoveItemAsync(InicializarUrl.Usuario);
            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
            _http.DefaultRequestHeaders.Authorization = null;
        }
    }
}
