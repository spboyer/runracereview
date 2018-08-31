using System;
using System.Globalization;
using Bogus;

namespace runracereview.Model
{
  public class RaceLoader
  {
    private readonly IRaceRepository _repository;

    public RaceLoader(IRaceRepository repository)
    {
      _repository = repository;
    }

    public void LoadData()
    {
      var distances = new[] { "5k", "10k", "Half", "Marathon", "Marathon", "Ultra" };
      var runType = new[] { "Run", "Trail", "Trail/Road" };

      var faker = new Faker<Race>()
      .RuleFor(r => r.Distances, f => new string[] { f.PickRandom(distances), f.PickRandom(distances) })
      .RuleFor(r => r.RaceType, f => new string[] { f.PickRandom(runType) })
      .RuleFor(r => r.RaceDate, f => new System.DateTimeOffset(f.Date.Between(new DateTime(2019, 1, 1), new DateTime(2019, 12, 31))))
      .RuleFor(r => r.Created, new System.DateTimeOffset(System.DateTime.Now))
      .RuleFor(r => r.Url, f => f.Internet.Url())
      .RuleFor(r => r.Country, "United States")
      .RuleFor(r => r.Location, f => f.Address.StreetAddress())
      .RuleFor(r => r.State, f => f.Address.State())
      .RuleFor(r => r.Name, f => string.Concat(f.Company.CatchPhrase(), " Race"));

      var races = faker.Generate(50);

      races.ForEach(race => _repository.Create(race));

    }
  }
}