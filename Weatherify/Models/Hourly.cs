namespace Weatherify.Models
{
  public class Hourly {
    public Guid Id {get; set;}
    public List<DateTime>? Time {get; set;}

    public List<int>? CloudCover {get; set;}
    public List<int>? PrecipitationProbability {get; set;}
    public List<int>? RelativeHumidityTwoM {get; set;}

    public List<double>? PressureMsl {get; set;}
    public List<double>? SurfacePressure {get; set;}
    public List<double>? TemperatureTwoM {get; set;}
    public List<double>? Visibility {get; set;}
  }
}
