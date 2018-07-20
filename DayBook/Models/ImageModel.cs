using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayBook.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageByte { get; set; }

        public int DayBookModelId { get; set; }
        public virtual DayBookModel DayBookModel { get; set; }
    }
}