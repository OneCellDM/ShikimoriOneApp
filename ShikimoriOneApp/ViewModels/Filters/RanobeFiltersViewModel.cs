using ReactiveUI;
using ShikimoriOneApp.Filters;
using ShikimoriSharp.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace ShikimoriOneApp.ViewModels.Filters
{

    public class RanobeFiltersViewModel : FilterBase
    {
        public MangaRequestSettings GetRequestSettings ( int page )
        {

            return new()
            {
                page = page,
                limit = 50,
                kind = Kind?.GetActiveValueString(),
                status = Status?.GetActiveValueString(),
                genre = Genres?.GetActiveValueString(),
                order = Order?.ElementAt(SelectedOrderIndex)?.Value,
                season = Seasons?.GetActiveValueString(),

            };
        }

       
        public RanobeFiltersViewModel()
        {
            LoadData("ranobe");
            Subscribe();
            SelectedOrderIndex=0;   
            RatingIsVisible = true;
            EnableOrDisableEvents(true);
        }

    }
}