namespace Viamatica.Blog.WASM.Modelos
{
    public class RespuestaAutenticacion
    {
        public bool isSuccess { get; set; }
        public string? Token { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
