using System.ComponentModel.DataAnnotations;

namespace Viamatica.Blog.WASM.Modelos
{
    public class Comentario
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Contenido { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int PublicacionId { get; set; }
        
        
    }
}
