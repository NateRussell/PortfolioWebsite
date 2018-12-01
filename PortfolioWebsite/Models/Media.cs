using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite.Models
{
    public class Media
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string FilePathRaw { get; set; }

        [Required]
        [StringLength(100)]
        public string FilePathEncoded { get; set; }

        [Required]
        [StringLength(100)]
        public string FilePathThumbnail { get; set; }

        [Required]
        public bool Thumbnail { get; set; }

        [Required]
        public bool Featured { get; set; }

        [Required]
        public bool Hidden { get; set; }

        [ForeignKey("Work")]
        public int WorkID { get; set; }
        public Work Work { get; set; }
    }
}
