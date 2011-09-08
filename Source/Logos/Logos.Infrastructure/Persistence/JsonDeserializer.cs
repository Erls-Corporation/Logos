using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System;
namespace Logos.Infrastructure.Persistence
{
    public sealed class JsonDeserializer
    {
        public JsonDeserializer()
        {
        }

        public dynamic Deserialize(Type deserializedType, string jsonValue)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonValue)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(deserializedType);
                return serializer.ReadObject(ms);
            }
        }
    }
}