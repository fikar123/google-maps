using System.Runtime.Serialization;
using GoogleMapsApi.Common;

namespace GoogleMapsApi.Serialization
{
	public interface IObjectSerializerFactory
	{
		XmlObjectSerializer GetSerializer<TResponseType>(ResponseOutputType outputType) where TResponseType : class;
	}
}