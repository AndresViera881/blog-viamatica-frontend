using Viamatica.Blog.WASM.Modelos;

namespace Viamatica.Blog.WASM.Servicios.IServicio
{
    public interface IPublicacionService
    {
        public Task<IEnumerable<Publicacion>> GetPublicaciones();
        public Task<Publicacion>GetByIdPublicacion(int publicacionId);
        public Task<Publicacion>PostPublicacion(Publicacion publicacion);
        public Task<Publicacion> PutPublicacion(int id, Publicacion publicacion);
        public Task<bool> DeletePublicacion(int id);
        public Task<string> UploadImage(MultipartFormDataContent content);


        public Task<IEnumerable<Comentario>> GetComentarios();
        public Task<IEnumerable<Comentario>> GetComentariosByPublicacion(int publicacionId);
        public Task<Comentario> PostComentario(Comentario comentario);

    }
}
