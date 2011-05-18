using System.Runtime.Serialization;
using GoogleMapsApi.Common;

namespace GoogleMapsApi.Serialization
{
	interface IObjectSerializerFactory
	{
		XmlObjectSerializer GetSerializer<TResponseType>(ResponseOutputType outputType) where TResponseType : class;
	}
}