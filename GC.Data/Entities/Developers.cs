using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCatalog.Data.Entities
{
    public class Developers : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Developer")]
        public string Name { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string Founder { get; set; }
        
        [DataType(dataType: DataType.Date)]
        public DateTime Founded { get; set; }
        public virtual ICollection<VideoGame> VideoGames { get; set; }
    }
}
