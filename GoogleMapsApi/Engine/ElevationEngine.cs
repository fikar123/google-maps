using System;
using System.Linq;
using System.Collections.Specialized;
using System.Net;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Elevation.Request;
using GoogleMapsApi.Entities.Elevation.Response;

namespace GoogleMapsApi.Engine
{
	public class ElevationEngine : MapsAPIGenericEngine
	{
		private static readonly Uri ElevationUri;

		static ElevationEngine()
		{
			ElevationUri = new Uri(BaseUri, "elevation/");
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


		protected override Uri GetUri(MapsBaseRequest request)
		{
			return new Uri(ElevationUri, request.Output.ToString().ToLower());
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