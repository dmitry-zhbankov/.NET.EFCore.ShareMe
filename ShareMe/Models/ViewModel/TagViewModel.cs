using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Models.Entity;

namespace ShareMe.Models.ViewModel
{
    public class TagViewModel
    {
        public Tag Tag { get; set; }
        public bool IsChecked { get; set; }
    }
}
