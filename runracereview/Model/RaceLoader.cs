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
      var imgs = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };
      var stars = new[] { 1, 2, 3, 4, 5 };

      var rev = new Faker<Review>()
        .RuleFor(r => r.ReviewerId, new Guid().ToString())
        .RuleFor(r => r.Stars, f => f.PickRandom(stars))
        .RuleFor(r => r.ReviewText, f => f.Rant.Review());

      var faker = new Faker<Race>()
      .RuleFor(r => r.Distances, f => new string[] { f.PickRandom(distances), f.PickRandom(distances) })
      .RuleFor(r => r.RaceType, f => new string[] { f.PickRandom(runType) })
      .RuleFor(r => r.RaceDate, f => new System.DateTimeOffset(f.Date.Between(new DateTime(2019, 1, 1), new DateTime(2019, 12, 31))))
      .RuleFor(r => r.Created, new System.DateTimeOffset(System.DateTime.Now))
      .RuleFor(r => r.Url, f => f.Internet.Url())
      .RuleFor(r => r.Country, "United States")
      .RuleFor(r => r.Location, f => f.Address.StreetAddress())
      .RuleFor(r => r.State, f => f.Address.State())
      .RuleFor(r => r.Demo, true)
      .RuleFor(r => r.MainImage, f => $"/images/stock/{f.PickRandom(imgs)}.jpeg")
      .RuleFor(r => r.Reviews, f => rev.Generate(f.PickRandom(stars)).ToArray())
      .RuleFor(r => r.Name, f =>  $"{f.Company.CatchPhrase()} race");

      var races = faker.Generate(24);

      races.ForEach(race => _repository.Create(race));
    }

  }
}