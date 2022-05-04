using Bogus;
using Nudes.SeedMaster.Interfaces;
using SChallengeAPI.Domain;
using SChallengeAPI.Services;

namespace SChallengeAPI.Data;

class FullSeed : ISeed<Db>
{
    private readonly PasswordHasher hasher;

    public FullSeed(PasswordHasher hasher)
    {
        this.hasher=hasher;
    }

    public Task Seed(Db db)
    {
        var (hash, salt) = hasher.Hash("string");
        db.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            Username = "string",
            Hash = hash,
            Salt = salt
        });
        Faker<User> userGenerator = new Faker<User>()
            .RuleFor(d => d.Username, d => d.Person.UserName)
            .RuleFor(d => d.Hash, hash)
            .RuleFor(d => d.Salt, salt);

        var users = userGenerator.Generate(10);
        db.Users.AddRange(users);

        Faker<Event> eventGenerator = new Faker<Event>()
            .RuleFor(d => d.Name, d => d.Company.CompanyName())
            .RuleFor(d => d.Creator, d => d.PickRandom(users))
            .RuleFor(d => d.Date, d => d.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(100)))
            .RuleFor(d => d.Duration, d => d.Date.Timespan(TimeSpan.FromDays(1)))
            .RuleFor(d => d.Participants, d => d.Random.Number(1000));

        var events = eventGenerator.Generate(1000);
        db.Events.AddRange(events);

        return Task.CompletedTask;
    }

    public Task Seed(object context) => Seed(context as Db);
}
