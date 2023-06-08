using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCatalog.Data.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(30, MinimumLength =2)]
        [Display(Name = "Genre")]
        public string Value { get; set; }
        public virtual ICollection<VideoGame> VideoGames { get; set; }

    }
}
