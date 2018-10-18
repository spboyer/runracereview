using System;
using MongoDB.Bson.Serialization.Attributes;

namespace runracereview
{
  public class Review
  {
    [BsonElement("stars")]
    public int Stars { get; set; }

    [BsonElement("reviewerId")]
    public string ReviewerId { get; set; }

    [BsonElement("reviewText")]
    public string ReviewText { get; set; }

    [BsonElement("created")]
    public DateTimeOffset Created { get; set; }
  }
}