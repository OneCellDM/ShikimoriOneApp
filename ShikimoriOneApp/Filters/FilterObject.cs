using Newtonsoft.Json;
using ReactiveUI;
using ShikimoriOneApp.Filters;
using System;
using System.Collections.Generic;

namespace ShikimoriOneApp.Filters
{
    public class FilterObject : ReactiveObject,IFilterObject
    {
        private bool _Active;
        [JsonProperty("value")]
        public string? Value { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("type")]
        public List<string>? Type { get; set; }

        public event IFilterObject.FilterActive? FilterActiveEvent;

        public bool FilterActiveEventIsEnable { get;set; }
        public bool Active { 
            get =>_Active;
            set 
            {
                this.RaiseAndSetIfChanged(ref _Active,value);
       
                if(FilterActiveEventIsEnable)
                        FilterActiveEvent?.Invoke(this);
            } 
        }
        
    }
}