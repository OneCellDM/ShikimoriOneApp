using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikimoriOneApp.Filters
{
    public interface IFilterObject
    {
       
        public string? Title { get; set; }
        public string? Value { get; set;}
        public List<string>? Type { get; set; }
        public bool FilterActiveEventIsEnable { get; set; }
        public delegate void FilterActive (FilterObject sender);
        public event FilterActive FilterActiveEvent;
        public bool Active{get;set;}
       
    }
}
