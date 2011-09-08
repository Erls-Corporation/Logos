using System.Runtime.Serialization;
namespace Logos.Domain.Events
{
    [DataContract]
    public class DomainEvent : IMessage
    {
        [DataMember]
        public int Version { get; set; }
    }
}