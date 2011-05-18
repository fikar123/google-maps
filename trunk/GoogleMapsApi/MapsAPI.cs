using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Engine;
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
		private static readonly IMapsAPIEngine MapsAPIEngine;

		static MapsAPI()
		{
			MapsAPIEngine = new MapsAPIEngine();
		}

		public static GeocodingResponse GetGeocode(GeocodingRequest request)
		{
			return MapsAPIEngine.GetGeocode(request);
		}

		public static DirectionsResponse GetDirections(DirectionsRequest request)
		{
			return MapsAPIEngine.GetDirections(request);
		}

		public static ElevationResponse GetElevation(ElevationRequest request)
		{
			return MapsAPIEngine.GetElevation(request);
		}

		public static PlacesResponse GetPlace(PlacesRequest request)
		{
			return MapsAPIEngine.GetPlace(request);
		}
	}
}
