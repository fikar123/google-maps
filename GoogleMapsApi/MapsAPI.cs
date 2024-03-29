﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleMapsApi.Engine;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Elevation.Request;
using GoogleMapsApi.Entities.Elevation.Response;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.Serialization;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Places.Request;
using GoogleMapsApi.Entities.Places.Response;

namespace GoogleMapsApi
{
	/// <summary>
	/// Static facade class to MapsAPI
	/// </summary>
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
