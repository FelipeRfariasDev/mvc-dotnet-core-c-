using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMvc.Models
{
    public class Comentarios
    {
        public int Id { get; set; }

        public string Comentario { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
