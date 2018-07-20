using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayBook.Models.ViewModels
{
    public class DayRecordViewModel
    {
        public string DayRecord { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageByte { get; set; }

        public IEnumerable<ImageModel> ImageModels { get; set; }
    }
}