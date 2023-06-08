using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCatalog.Data.Entities
{
    public class VideoGame : BaseEntity
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set;  }

        [Display(Name = "Release Date")]
        [DataType(dataType: DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Range(1.0, 10.0)]
        [Display(Name = "User Rating")]
        public double? UserRating { get; set; }

        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [Display(Name = "Developers")]
        public int? DevelopersId { get; set; }
        public virtual Developers Developers { get; set; }
    }
}
