using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayBook.Models.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<DayBookModel> Records { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}