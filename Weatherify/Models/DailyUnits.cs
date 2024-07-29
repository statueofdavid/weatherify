namespace Weatherify.Models
{
  public class DailyUnits {
    public int WeatherId {get; set;}

    public string? Time {get; set;}
    public string? Sunrise {get; set;}
    public string? Sunset {get; set;}

    public char? DaylightDuration {get; set;}
    public char? PrecipitationProbabilityMax {get; set;}
  }
}
