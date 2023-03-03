﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using projecto_iefp.Models;

namespace projecto_iefp.Pages
{
    public class ComentariosModel : PageModel
    {
        public IEnumerable<Models.Comentario> comentario { get; set; }

        

        public void OnGet()
        {
            livrosContext context = new livrosContext();
            comentario = context.GetAllComentarios();

        }

           
    }
}
