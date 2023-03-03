using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using projecto_iefp.Models;


namespace projecto_iefp.Pages
{
    public class FiltroModel : PageModel
    {
        public IEnumerable<Models.Comentario> comentario { get; set; }
        public IEnumerable<Models.livro> livro { get; set; }

        public void OnGet(string searchTerm)
        {
            livrosContext context = new livrosContext();
            comentario = context.searchComentario(searchTerm);
            livro = context.GetAllLivros();
            
        }


    }

   
}
