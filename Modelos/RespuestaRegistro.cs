﻿namespace Viamatica.Blog.WASM.Modelos
{
    public class RespuestaRegistro
    {
        public bool RegistroCorrecto { get; set; }
        public IEnumerable<string>? Errores { get; set; }
    }
}
