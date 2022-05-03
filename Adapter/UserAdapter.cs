using SChallengeAPI.Features.Users;
using SChallengeAPI.Models;

namespace SChallengeAPI.Adapter;

class UserAdapter : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddUserRequest, Domain.User>()
            .Ignore(d => d.Id)
            .Ignore(d => d.Hash)
            .Ignore(d => d.Salt)
            .Ignore(d => d.Events);

        config.NewConfig<Domain.User, Reference>()
            .Map(d => d.Name, d => d.Username);
    }
}
