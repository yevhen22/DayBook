using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayBook.Models
{
    public class DayBookModel
    {
        [Key]
        public int DayBookModelId { get; set; }

        [MaxLength(500)]
        public string DayRecord { get; set; }
        public DateTime CreationTime { get; set; }


        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ImageModel> ImageModels { get; set; }


    }
}