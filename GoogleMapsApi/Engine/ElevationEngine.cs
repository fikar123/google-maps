using System;
using System.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Elevation.Request;
using GoogleMapsApi.Entities.Elevation.Response;

namespace GoogleMapsApi.Engine
{
	public class ElevationEngine : MapsAPIGenericEngine
	{
		private static readonly string ElevationUrl;

		static ElevationEngine()
		{
			ElevationUrl = @"elevation/";
		}

		public IAsyncResult BeginGetElevation(ElevationRequest request, AsyncCallback asyncCallback, object state)
		{
			return BeginQueryGoogleAPI<ElevationRequest, ElevationResponse>(request, asyncCallback, state);
		}

		public ElevationResponse EndGetElevation(IAsyncResult asyncResult)
		{
			return EndQueryGoogleAPI<ElevationResponse>(asyncResult);
		}

		public ElevationResponse GetElevation(ElevationRequest request)
		{
			return QueryGoogleAPI<ElevationRequest, ElevationResponse>(request);
		}
		
		public Task<ElevationResponse> GetElevationAsync(ElevationRequest request)
		{
			return QueryGoogleAPIAsync<ElevationRequest, ElevationResponse>(request);
		}


		protected override Uri GetUri(MapsBaseRequest request)
		{
			string scheme = request.IsSSL ? "https://" : "http://";

			Uri uri = new Uri(scheme + BaseUrl + ElevationUrl + request.Output.ToString().ToLower());

			return uri;
		}

		protected override void ConfigureUnderlyingWebClient(WebClient wc, MapsBaseRequest baseRequest)
		{
			ElevationRequest request = (ElevationRequest)baseRequest;

			NameValueCollection queryString = wc.QueryString;

			if (request.Locations != null)
			{
				var locationsStrings = request.Locations.Select(location => string.Format("{0},{1}", location.Latitude, location.Longitude));

				string allLocations = string.Join("|", locationsStrings);
				queryString.Add("locations", allLocations);
			}
			else if (request.Path != null)
			{
				var points = request.Path.Select(path => string.Format("{0},{1}", path.Latitude, path.Longitude));
				queryString.Add("path", string.Join("|", points));
			}
			else
			{
				throw new ArgumentException("Locations or Path must be specified");
			}

			queryString.Add("sensor", request.Sensor.ToString().ToLower());
		}
	}
}