# Quick start

# Introduction #

Some code samples -

The principle is simple -
Fill the request with the data, call the static method on **MapsAPI** and the response object arrives.


# Details #

```
//Directions
DirectionsRequest directionsRequest = new DirectionsRequest()
{
        Origin = "NYC, 5th and 39",
        Destination = "Philladephia, Chesnut and Wallnut",
};

        DirectionsResponse directions = MapsAPI.GetDirections(directionsRequest);


//Geocode
GeocodingRequest geocodeRequest = new GeocodingRequest()
{
        Address = "new york city",
};


GeocodingResponse geocode = MapsAPI.GetGeocode(geocodeRequest);

Console.WriteLine(geocode);

//Elevation
ElevationRequest elevationRequest = new ElevationRequest()
{
        Locations = new Location[] { new Location(54, 78) },
};


ElevationResponse elevation = MapsAPI.GetElevation(elevationRequest);

Console.WriteLine(elevation);
```