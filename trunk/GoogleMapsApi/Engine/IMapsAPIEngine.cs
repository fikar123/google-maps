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
	public interface IMapsAPIEngine
	{
		GeocodingResponse GetGeocode(GeocodingRequest request);
		DirectionsResponse GetDirections(DirectionsRequest request);
		ElevationResponse GetElevation(ElevationRequest request);
		PlacesResponse GetPlace(PlacesRequest request);
	}
}