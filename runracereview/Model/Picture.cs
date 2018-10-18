using System;
using MongoDB.Bson.Serialization.Attributes;

namespace runracereview
{
  public class Picture
  {
    [BsonElement("url")]
    public string Url { get; set; }

    [BsonElement("created")]
    public DateTimeOffset Created { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("reviewerId")]
    public string ReviewerId { get; set; }
  }
}