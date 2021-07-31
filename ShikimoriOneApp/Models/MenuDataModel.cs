using Newtonsoft.Json;
using ShikimoriOneApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShikimoriOneApp.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class FiltersDataModel
    {
       

        private List<FilterObject>? _Seasons;

        [JsonProperty("status")]
        public List<FilterObject>? Status;

        [JsonProperty("kind")]
        public List<FilterObject>? Kind;

        [JsonProperty("order")]
        public List<FilterObject>? Order;

        [JsonProperty("genres")]
        public List<FilterObject>? Genres;

        [JsonProperty("rating")]
        public List<FilterObject>? Rating;

        public List<FilterObject>? Seasons
        {
            get
            {
                if ( _Seasons is null )
                {
                    _Seasons = GenSeasons();
                }

                return _Seasons;
            }
        }

        public class YearConverter
        {
            public int Year { get; private set; }

            public YearConverter ( int year ) => Year = year;

            public string GetString () => Year.ToString();

            public string GetString ( string addText, bool isStart = true ) =>
                isStart ? string.Format("{0}{1}", addText, Year) : string.Format("{0}{1}", Year, addText);

            public string GetString ( int val, string addText, bool isStart = true ) =>
               isStart ? string.Format("{0}{1}", addText, val) : string.Format("{0}{1}", val, addText);

            public string GetRangeString ( int start, int end, char separate ) =>
                string.Format("{0}{1}{2}", start, separate, end);

            public string GetRangeString ( int start, int end, char separate, string addText ) =>
               string.Format("{0}{1}{2}{3}", start, separate, end, addText);

            public YearConverter GetYearPlus( int value )=> new YearConverter(Year + value);
            
            public YearConverter GetYearMinus ( int value )=> new YearConverter(Year - value);
            
        }

        private List<FilterObject> GenSeasons ()
        {
            List<FilterObject> res = new();
            var converter = new YearConverter(DateTime.Now.Year);
            string [,] seasons = new string [,]
            {
                { converter.GetString("winter_"), converter.GetString("Зима ")},
                { converter.GetString("spring_"), converter.GetString("Весна ")},
                { converter.GetString("summer_"), converter.GetString("Лето ")},
                { converter.GetString("fall_"),   converter.GetString("Осень ")},
                { converter.GetYearPlus(1).GetString("winter_")  ,converter.GetYearPlus(1).GetString("Зима ")},
                {
                    converter.GetString(),  
                    converter.GetString(" Год",false) 
                },
                {   
                    converter.GetYearMinus(1).GetString(),  
                    converter.GetYearMinus(1).GetString(" Год",false) 
                },
                {
                    converter.GetRangeString(converter.Year-3,converter.Year-2,'_'),
                    converter.GetRangeString(converter.Year-3,converter.Year-2,'-'," Год"),
                },
                {
                   
                    converter.GetRangeString(converter.Year-8,converter.Year-4,'_'),
                    converter.GetRangeString(converter.Year-8,converter.Year-4,'-'," Год")
                },
                {

                    converter.GetRangeString(2000,converter.Year-9,'_'),
                    converter.GetRangeString(2000,converter.Year-9,'-'," Год")
                },
                { "199x","199x Года" },
                { "198x","198x Года" },
                { "ancient","Более старые" },
            };
            
            for ( int i = 1; i <= DateTime.Now.Month; i ++ )
            {
                if ( i <= 2 )
                    res.Add(new()
                    {
                        Title = seasons [0, 1],
                        Value = seasons [0, 0]
                    });
                else if ( i >= 3 & i <= 5 )
                    res.Add(new()
                    {
                        Title = seasons [1, 1],
                        Value = seasons [1, 0],
                    });
                else if ( i >= 6 & i <= 8 )
                    res.Add(new()
                    {
                        Title = seasons [2, 1],
                        Value = seasons [2, 0],
                    });
                else if ( i >= 9 & i <= 11 )
                    res.Add(new()
                    {
                        Title = seasons [3, 1],
                        Value = seasons [3, 0],
                    });
                else if ( i == 12 )
                    res.Add(new()
                    {
                        Title = seasons [4, 1],
                        Value = seasons [4, 0],
                    });
            }
            res.Reverse();
            for ( int i = 5; i < seasons.GetLength(0); i++ ) 
                res.Add(new()
                {
                    Title = seasons [i, 1],
                    Value = seasons [i, 0],
                });   
            
            res = res.GroupBy(x => x.Value).Select(x => x.First()).ToList();
            
            return res;
        }
    }
}