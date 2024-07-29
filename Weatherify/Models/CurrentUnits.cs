namespace Weatherify.Models
{
  public class CurrentUnits {
    public char? WindDirectionTenM {get; set;}
    public int WeatherId {get; set;}

    public string? Time {get; set;}
    public string? Interval {get; set;}
    public string? TemperatureTwoM {get; set;}

    public string? IsDay {get; set;}
    public string? Precipitation {get; set;}
    public string? PressureMsl {get; set;}

    public string? SurfacePressure {get; set;}
    public string? WindSpeedTenM {get; set;}
    public string? WindGustsTenM {get; set;}
  }
}
