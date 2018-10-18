using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace runracereview.Model
{
  public class Race
  {
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    // mongodb stores dates in UTC format date before storing.
    [BsonElement("raceDate")]
    public DateTimeOffset RaceDate { get; set; }

    [BsonElement("location")]
    public string Location { get; set; }

    [BsonElement("state")]
    public string State { get; set; }

    [BsonElement("country")]
    public string Country { get; set; }

    // Run, Trail Run ?
    [BsonElement("raceType")]
    public string[] RaceType { get; set; }

    // 5k, 10k, Half Marathon, Marathon, Ultra, etc.
    [BsonElement("distances")]
    public string[] Distances { get; set; }

    [BsonElement("url")]
    public string Url { get; set; }

    [BsonElement("mainImage")]
    public string MainImage { get; set; }

    [BsonElement("demo")]
    public bool Demo { get; set; }

    [BsonElement("created")]
    public DateTimeOffset Created { get; set; }

    [BsonElement("reviews")]
    public Review[] Reviews { get; set; }

    [BsonElement("pictures")]
    public Picture[] Pictures { get; set; }

    public int AverageRating
    {
      get
      {
        return Convert.ToInt32(Math.Round(Reviews.Average(r => r.Stars), 0));
      }
    }
  }
}