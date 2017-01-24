using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtProject.Models
{
    public class Painter
    {
        public int PainterID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}