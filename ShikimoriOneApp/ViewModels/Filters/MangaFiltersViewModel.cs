using ReactiveUI;
using ShikimoriOneApp.Filters;
using ShikimoriSharp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ShikimoriOneApp.ViewModels.Filters
{

    public class MangaFiltersViewModel : FilterBase
    {

        public MangaRequestSettings GetRequestSettings(int page)=>
            new ()
            {       
                page = page,
                limit = 50,
                kind=Kind?.GetActiveValueString(),
                status=Status?.GetActiveValueString(),
                genre=Genres?.GetActiveValueString(),
                order=Order?.ElementAt(SelectedOrderIndex)?.Value,
                season = Seasons?.GetActiveValueString(),
            };
   
      
        public MangaFiltersViewModel(){
        
            LoadData("manga");
            Subscribe();
            SelectedOrderIndex=0;       
            EnableOrDisableEvents(true);
        }
        
    }
}