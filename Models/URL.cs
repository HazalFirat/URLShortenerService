using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortenerService.Models
{
    public class URL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [MaxLength(2000)]
        public string OriginalURL { get; set; }
        [MaxLength(100)]
        public string ShortURL { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
    }
}
