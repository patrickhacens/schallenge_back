using SChallengeAPI.Features.Events;

namespace SChallengeAPI.Adapter;

class EventAdapter : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddEventRequest, Domain.Event>();
        config.NewConfig<Domain.Event, Models.Event>();

        config.NewConfig<EventInformation, Domain.Event>();

        config.NewConfig<Domain.Event, Models.EventDetail>()
            .Map(d => d.Creator, d => d.Creator);
    }
}
