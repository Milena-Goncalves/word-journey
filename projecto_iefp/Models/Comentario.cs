using System;
namespace projecto_iefp.Models;

public class Comentario
{
	private livrosContext context;

        public string? titulo { get; set; }
        public string? nome { get; set; }
        public string? comentario { get; set; }
        public string? email { get; set; }

}

