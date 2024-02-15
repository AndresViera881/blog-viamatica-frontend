using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Viamatica.Blog.WASM.Helper;
using Viamatica.Blog.WASM.Modelos;
using Viamatica.Blog.WASM.Servicios.IServicio;

namespace Viamatica.Blog.WASM.Servicios
{
    public class PublicacionService : IPublicacionService
    {
        private readonly HttpClient _http;

        public PublicacionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Publicacion>> GetPublicaciones()
        {
            var response = await _http.GetAsync($"{InicializarUrl.UrlBaseApi}api/Publicacion");
            var content = await response.Content.ReadAsStringAsync();
            var publicaciones = JsonConvert.DeserializeObject<IEnumerable<Publicacion>>(content);
            return publicaciones!;
        }

        public async Task<Publicacion> GetByIdPublicacion(int id)
        {
            var response = await _http.GetAsync($"{InicializarUrl.UrlBaseApi}api/Publicacion/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var publicacion = JsonConvert.DeserializeObject<Publicacion>(content);
                return publicacion!;
            }
            else 
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel!.ErrorMessage);
            }
        }

        public async Task<Publicacion> PostPublicacion(Publicacion publicacion)
        {
            var content = JsonConvert.SerializeObject(publicacion);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync($"{InicializarUrl.UrlBaseApi}api/Publicacion", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Publicacion>(contentTemp);
                return result!;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel!.ErrorMessage);
            }
        }

        public async Task<bool> DeletePublicacion(int id)
        {
            var response = await _http.DeleteAsync($"{InicializarUrl.UrlBaseApi}api/Publicacion/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel!.ErrorMessage);
            }
        }

        public async Task<Publicacion> PutPublicacion(int id, Publicacion publicacion)
        {
            var content = JsonConvert.SerializeObject(publicacion);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _http.PatchAsync($"{InicializarUrl.UrlBaseApi}api/Publicacion/{id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentPublicacion = await response.Content.ReadAsStringAsync();
                var publicacionPost = JsonConvert.DeserializeObject<Publicacion>(contentPublicacion);
                return publicacionPost!;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel!.ErrorMessage);
            }
        }

        public async Task<string> UploadImage(MultipartFormDataContent content)
        {
            var postRequest = await _http.PostAsync($"{InicializarUrl.UrlBaseApi}api/Upload", content);
            var result = await postRequest.Content.ReadAsStringAsync();
            if (!postRequest.IsSuccessStatusCode)
            {
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(result);
                throw new Exception(errorModel!.ErrorMessage);
            }
            else 
            {
                var imagenPost = Path.Combine(InicializarUrl.UrlBaseApi, result);
                return imagenPost;
            }
        }

        public async Task<IEnumerable<Comentario>> GetComentarios()
        {
            var response = await _http.GetAsync($"{InicializarUrl.UrlBaseApi}api/Comentario");
            var content = await response.Content.ReadAsStringAsync();
            var comentarios = JsonConvert.DeserializeObject<IEnumerable<Comentario>>(content);
            return comentarios!;
        }

        public async Task<Comentario> PostComentario(Comentario comentario)
        {
            var content = JsonConvert.SerializeObject(comentario);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync($"{InicializarUrl.UrlBaseApi}api/Comentario", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Comentario>(contentTemp);
                return result!;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel!.ErrorMessage);
            }
        }

        public async Task<IEnumerable<Comentario>> GetComentariosByPublicacion(int publicacionId)
        {
            var response = await _http.GetAsync($"{InicializarUrl.UrlBaseApi}api/Comentario/GetComentariosByPublicacion/{publicacionId}");
            var content = await response.Content.ReadAsStringAsync();
            var comentarios = JsonConvert.DeserializeObject<IEnumerable<Comentario>>(content);
            return comentarios!;
        }
    }
}
