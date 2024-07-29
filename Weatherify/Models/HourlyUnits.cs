namespace Weatherify.Models
{
  public class HourlyUnits {
    public int Weather {get; set;}
    public string? Time {get; set;}

    public string? TemperatureTwoM {get; set;}
    public string? PressureMsl {get; set;}
    public string? Visibility {get; set;}

    public char? PrecipitationProbability {get; set;}
    public string? SurfacePressure {get; set;}

    public char? RelativeHumidityTwoM {get; set;}
    public char? CloudCover {get; set;}
  }
}
