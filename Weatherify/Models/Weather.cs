using System;

namespace Weatherify.Models
{
  public class Weather {
    public Guid Id {get; set;}

    public double Latitude {get; set;}
    public double Longitude {get; set;}

    public float GenerationtimeMs {get; set;}
    public double UtcOffSetSeconds {get; set;}
  
    public string TimezoneAbbr {get; set;}
    public double Elevation {get; set;}
  }
}
