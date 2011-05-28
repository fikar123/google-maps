using System;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.Serialization;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;

namespace GoogleMapsApi.Engine
{
	public class DirectionsEngine : MapsAPIGenericEngine
	{
		private static readonly string DirectionsUrl;

		static DirectionsEngine()
		{
			DirectionsUrl = @"directions/";
		}

		public IAsyncResult BeginGetDirections(DirectionsRequest request, AsyncCallback asyncCallback, object state)
		{
			return BeginQueryGoogleAPI<DirectionsRequest, DirectionsResponse>(request, asyncCallback, state);
		}

		public DirectionsResponse EndGetDirections(IAsyncResult asyncResult)
		{
			return EndQueryGoogleAPI<DirectionsResponse>(asyncResult);
		}

		public DirectionsResponse GetDirections(DirectionsRequest request)
		{
			return QueryGoogleAPI<DirectionsRequest, DirectionsResponse>(request);
		}

		protected override Uri GetUri(MapsBaseRequest request)
		{
			string scheme = request.IsSSL ? "https://" : "http://";

			Uri uri = new Uri(scheme + BaseUrl + DirectionsUrl + request.Output.ToString().ToLower());

			return uri;
		}

		protected override void ConfigureUnderlyingWebClient(WebClient wc, MapsBaseRequest baseRequest)
		{
			DirectionsRequest request = (DirectionsRequest) baseRequest;

			NameValueCollection queryString = wc.QueryString;

			if (!string.IsNullOrWhiteSpace(request.Origin))
			{
				queryString.Add("origin", request.Origin);
			}
			else
			{
				throw new ArgumentException("Must specify Origin");
			}

			if (!string.IsNullOrWhiteSpace(request.Destination))
			{
				queryString.Add("destination", request.Destination);
			}
			else
			{
				throw new ArgumentException("Must specify Destination");
			}

			if (request.Alternatives)
			{
				queryString.Add("alternatives", "true");
			}

			switch (request.Avoid)
			{
				case AvoidWay.Nothing:
					break;
				case AvoidWay.Tolls:
					queryString.Add("avoid", "tolls");
					break;
				case AvoidWay.Highways:
					queryString.Add("avoid", "highways");
					break;
				default:
					throw new ArgumentException("Unknown value for 'AvoidWay' enum");
			}

			if (!string.IsNullOrWhiteSpace(request.Language))
			{
				queryString.Add("language", request.Language);
			}

			queryString.Add("sensor", request.Sensor.ToString().ToLower());

			if (request.Waypoints != null)
			{
				string wayPoints = string.Join("|", request.Waypoints);
				queryString.Add("waypoints", wayPoints);
			}

			switch (request.TravelMode)
			{
				case TravelMode.Driving:
					queryString.Add("mode", "driving");
					break;
				case TravelMode.Bicycling:
					queryString.Add("mode", "bicycling");
					break;
				case TravelMode.Walking:
					queryString.Add("mode", "walking");
					break;
				default:
					throw new ArgumentException("Unknown value for 'TravelMode' enum");
			}
		}

		
	}
}