using System;
using GoogleMapsApi.Directions.Request;
using GoogleMapsApi.Directions.Response;
using GoogleMapsApi.Elevation.Request;
using GoogleMapsApi.Elevation.Response;
using GoogleMapsApi.Geocoding.Request;
using GoogleMapsApi.Geocoding.Response;
using GoogleMapsApi.Places.Request;
using GoogleMapsApi.Places.Response;

namespace GoogleMapsApi.Engine
{
	/// <summary>
	/// This class is a facade to all Maps engines.
	/// </summary>
	public class MapsAPIEngine : IMapsAPIEngine
	{
		public GeocodingResponse GetGeocode(GeocodingRequest request)
		{
			return new GeocodingEngine().GetGeocode(request);
		}

		public DirectionsResponse GetDirections(DirectionsRequest request)
		{
			return new DirectionsEngine().GetDirections(request);
		}

		public ElevationResponse GetElevation(ElevationRequest request)
		{
			return new ElevationEngine().GetElevation(request);
		}

		public PlacesResponse GetPlace(PlacesRequest request)
		{
			throw new NotImplementedException();
		}
	}
}