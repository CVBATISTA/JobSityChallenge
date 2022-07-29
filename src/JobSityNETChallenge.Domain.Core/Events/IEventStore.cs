using NetDevPack.Messaging;

namespace JobSityNETChallenge.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}