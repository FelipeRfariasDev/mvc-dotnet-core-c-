using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMvc.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime? Data { get; set; }

        public string Comentario { get; set; }
        public List<Comentarios> Comentarios { get; set; }
    }
}
