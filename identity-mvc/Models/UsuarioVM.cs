﻿using Microsoft.AspNetCore.Components.Web;

namespace identity_mvc.Models
{
    public class UsuarioVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EsAdmin { get; set; }
    }
}
