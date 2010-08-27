using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Geocoding.Response;
using GoogleMapsApi.Geocoding.Request;
using GoogleMapsApi.Directions.Response;
using GoogleMapsApi.Directions.Request;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.Serialization;
using GoogleMapsApi.Elevation.Request;
using GoogleMapsApi.Elevation.Response;
using GoogleMapsApi.Places.Response;
using GoogleMapsApi.Places.Request;

namespace GoogleMapsApi
{
	public static class MapsAPI
	{
		private static readonly Uri GeocodeUri;
		private static readonly Uri DirectionsUri;
		private static readonly Uri ElevationUri;
		private static readonly Uri PlacesUri;


		static MapsAPI()
		{
			GeocodeUri = new Uri(GoogleApiConfigurations.BaseUri, "geocode/");
			DirectionsUri = new Uri(GoogleApiConfigurations.BaseUri, "directions/");
			ElevationUri = new Uri(GoogleApiConfigurations.BaseUri, "elevation/");
			PlacesUri = new Uri(GoogleApiConfigurations.BaseUri, "place/");
		}

		public static GeocodingResponse GetGeocode(GeocodingRequest request)
		{
			using (WebClient wc = new WebClient())
			{
				NameValueCollection queryString = wc.QueryString;

				if (request.Location != null &&
						string.IsNullOrWhiteSpace(request.Address))
				{
					throw new ArgumentException("Location OR Address is required");
				}
				else
				{
					if (request.Location != null)
					{
						queryString.Add("latlng",
								string.Format("{0},{1}", request.Location.Latitude, request.Location.Longitude));
					}
					if (!string.IsNullOrWhiteSpace(request.Address))
					{
						queryString.Add("address", request.Address);
					}
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




				Uri uri = new Uri(GeocodeUri, request.Output.ToString().ToLower());

				var stream = wc.OpenRead(uri);

				XmlObjectSerializer serializer = XmlObjectSerializerFactory.GetSerializer<GeocodingResponse>(request.Output);

				return (GeocodingResponse)serializer.ReadObject(stream);
			}
		}

		public static DirectionsResponse GetDirections(DirectionsRequest request)
		{
			using (WebClient wc = new WebClient())
			{

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

				Uri uri = new Uri(DirectionsUri, request.Output.ToString().ToLower());

				var stream = wc.OpenRead(uri);

				XmlObjectSerializer serializer = XmlObjectSerializerFactory.GetSerializer<DirectionsResponse>(request.Output);

				return (DirectionsResponse)serializer.ReadObject(stream);
			}

		}

		public static ElevationResponse GetElevation(ElevationRequest request)
		{
			using (WebClient wc = new WebClient())
			{
				NameValueCollection queryString = wc.QueryString;


				if (request.Locations != null)
				{
					var locationsStrings = request.Locations.Select(location => string.Format("{0},{1}", location.Latitude, location.Longitude));

					string allLocations = string.Join("|", locationsStrings);
					queryString.Add("locations", allLocations);
				}
				else if (request.Path != null)
				{
					var points = request.Path.Select(location => string.Format("{0},{1}"));
					queryString.Add("path", string.Join("|", points));
				}
				else
				{
					throw new ArgumentException("Locations or Path must be specified");
				}


				queryString.Add("sensor", request.Sensor.ToString().ToLower());


				Uri uri = new Uri(ElevationUri, request.Output.ToString().ToLower());

				var stream = wc.OpenRead(uri);

				XmlObjectSerializer serializer = XmlObjectSerializerFactory.GetSerializer<ElevationResponse>(request.Output);

				return (ElevationResponse)serializer.ReadObject(stream);
			}
		}

		public static PlacesResponse GetPlace(PlacesRequest request)
		{
			throw new NotImplementedException("Not implemented yet, check for newer version");
		}
	}
}
