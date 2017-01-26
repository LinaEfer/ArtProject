using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtProject.Models
{
    public class Picture
    {
        public int PictureID { get; set; }
        public int PainterID { get; set; }
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RealiseDate { get; set; }
        public string Description { get; set; }
        public virtual Painter Painter { get; set; }
    }
}