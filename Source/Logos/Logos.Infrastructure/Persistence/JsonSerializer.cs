using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
namespace Logos.Infrastructure.Persistence
{
    public sealed class JsonSerializer
    {
        public JsonSerializer()
        {
        }

        public string Serialize<T>(T data)
            where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
            
            using(MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, data);

                return Encoding.Default.GetString(stream.ToArray());
             }
        }
    }
}