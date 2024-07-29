namespace Weatherify.Models
{
  public class Current {
    public DateTime? Time {get; set;}
    public int WeatherId {get; set;}

    public int? Interval {get; set;}
    public int? WindDirectionTenM {get; set;}

    public int? IsDay {get; set;}
    public double Precipitation {get; set;}

    public double? PressureMsl {get; set;}
    public double? SurfacePressure {get; set;}

    public double? TemperatureTwoM {get; set;}
    public double? WindSpeedTenM {get; set;}
    public double? WindGustsTenM {get; set;}
  }
}
