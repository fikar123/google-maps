using System.Runtime.Serialization;
using GoogleMapsApi.Entities.Common;

namespace GoogleMapsApi.Serialization
{
	public interface IObjectSerializerFactory
	{
		XmlObjectSerializer GetSerializer<TResponseType>(ResponseOutputType outputType) 
			where TResponseType : class;
	}
}