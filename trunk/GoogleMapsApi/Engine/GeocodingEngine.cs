using System;
using System.IO;
using System.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;

namespace GoogleMapsApi.Engine
{
	public class GeocodingEngine : MapsAPIGenericEngine
	{
		private static readonly string GeocodeUri;

		static GeocodingEngine()
		{
			GeocodeUri = @"geocode/";
		}

		public IAsyncResult BeginGetGeocode(GeocodingRequest request, AsyncCallback asyncCallback, object state)
		{
			return BeginQueryGoogleAPI<GeocodingRequest, GeocodingResponse>(request, asyncCallback, state);
		}

		public GeocodingResponse EndGetGeocode(IAsyncResult asyncResult)
		{
			return EndQueryGoogleAPI<GeocodingResponse>(asyncResult);
		}

		public GeocodingResponse GetGeocode(GeocodingRequest request)
		{
			return QueryGoogleAPI<GeocodingRequest ,GeocodingResponse>(request);
		}



		override protected Uri GetUri(MapsBaseRequest request)
		{
			string scheme = request.IsSSL ? "https://" : "http://";

			Uri uri = new Uri(scheme + BaseUrl + GeocodeUri + request.Output.ToString().ToLower());

			return uri;
		}

		override protected void ConfigureUnderlyingWebClient(WebClient wc, MapsBaseRequest baseRequest)
		{
			GeocodingRequest request = (GeocodingRequest)baseRequest;

			NameValueCollection queryString = wc.QueryString;

			if (request.Location != null &&
					string.IsNullOrWhiteSpace(request.Address))
			{
				throw new ArgumentException("Location OR Address is required");
			}


			if (request.Location != null)
			{
				queryString.Add("latlng",
												string.Format("{0},{1}", request.Location.Latitude, request.Location.Longitude));
			}
			if (!string.IsNullOrWhiteSpace(request.Address))
			{
				queryString.Add("address", request.Address);
			}

			if (request.Bounds != null)
			{
				//Convert each bound to string
				var boundsStrings = request.Bounds.Select(
					bound =>
					string.Format("{0},{1}", bound.Latitude, bound.Longitude));

				queryString.Add("bounds", string.Join("|", boundsStrings));
			}

			if (!string.IsNullOrWhiteSpace(request.Region))
			{
				queryString.Add("region", request.Region);
			}

			if (!string.IsNullOrWhiteSpace(request.Language))
			{
				queryString.Add("language", request.Language);
			}

			queryString.Add("sensor", request.Sensor.ToString().ToLower());
		}
	}
}