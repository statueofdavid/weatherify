using System;

public class Weather {
  public Guid id {get; set;}

  public double latitude {get; set;}
  public double longitude {get; set;}

  public float generationtimeMs {get; set;}
  public double utcOffSetSeconds {get; set;}
  
  public string timezoneAbbr {get; set;}
  public double elevation {get; set;}
}
