
using Microsoft.Extensions.Logging;
using ShikimoriSharp.Classes;
using ShikimoriSharp.Information;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShikimoriOneApp
{
    public static class ShikikomoriApiHandler
    {        
        class Logger : Microsoft.Extensions.Logging.ILogger
        {
            public IDisposable BeginScope<TState> ( TState state )
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled ( Microsoft.Extensions.Logging.LogLevel logLevel )
            {
                return true;
            }

            public void Log<TState> ( Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter )
            {
                Debug.WriteLine(state);
            }

         
        }

       
    
     
        private static bool _IsInit = false;
        private static ShikimoriSharp.ShikimoriClient? _ApiClient;
        public static ShikimoriSharp.Bases.AccessToken? TokenObject { get; set; }
        public static ShikimoriSharp.ShikimoriClient? ApiClient { get; set; }
 
       static ShikikomoriApiHandler ()
        {
            ApiClient = new ShikimoriSharp.ShikimoriClient(

                   new Logger(),
                   new("ShikikomoriOneCellDMApp", "To5K_6HfyRpSp_ZpOap8WReJFkjv7O3_Wj7dKl4_X90", "s-gUpfM5icFV18PI0ljmM0rpxBPiMqhNjC113C_0hFo")

                );
            
            
        }
       
      
        
       
    }
}
