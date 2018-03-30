using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACCUniversity.Models
{
    public class CampusLocations
    {
        [Key]
        public int campusId { get; set; }

        [Required]
        [Display(Name = "Campus Location")]
        [StringLength(50, MinimumLength = 1)]
        public string CampusCity { get; set; }
    }
}