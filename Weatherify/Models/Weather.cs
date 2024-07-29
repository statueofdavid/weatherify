namespace Weatherify.Models
{
  public class Weather {
    public Guid Id {get; set;}

    public double? Latitude {get; set;}
    public double? Longitude {get; set;}

    public float? GenerationTimeMs {get; set;}
    public double? UtcOffsetSeconds {get; set;}
  
    public string? TimezoneAbbr {get; set;}
    public double? Elevation {get; set;}
    
    public CurrentUnits? CurrentUnits {get; set;}
    public Current? Current {get; set;}
    public HourlyUnits? HourlyUnits {get; set;}

    public Hourly? Hourly {get; set;}
    public DailyUnits? DailyUnits {get; set;}
    public Daily? Daily {get; set;}

    public List<double>? DaylightDuration {get; set;}
    public List<double>? UvIndexMax {get; set;}
    public List<double>? PrecipitationProbabilityMax {get; set;}

    public override string ToString() {
      return $"Latitude: {Latitude}, Longitude: {Longitude}, GenerationTimeMs {GenerationTimeMs}, UtcOffsetSeconds: {UtcOffsetSeconds}, TimezoneAbbr: {TimezoneAbbr}, Elevation: {Elevation}";
    }
  }
}
