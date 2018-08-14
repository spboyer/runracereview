using System;

namespace runracereview.Model
{
  public class Race
  {
    public int Id { get; set; }
    public DateTime RaceDate { get; set; }
    public string Location { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    // Run, Trail Run ?
    public string RaceType { get; set; }

    // 5k, 10k, Half Marathon, Marathon, Ultra, etc.
    public string Distance { get; set; }

    public string Url { get; set; }

    public DateTime Created { get; set; }
  }
}