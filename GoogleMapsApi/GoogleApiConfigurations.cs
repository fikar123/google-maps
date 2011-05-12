using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleMapsApi
{
    internal static class GoogleApiConfigurations
    {
        public static readonly Uri BaseUri;

        
        static GoogleApiConfigurations()
        {
            BaseUri = new Uri("http://maps.google.com/maps/api/");
        }
    }
}
