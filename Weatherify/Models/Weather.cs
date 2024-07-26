using System;
public class Weather {
  public double? latitude {get; set;}
  public double? longitude {get; set;}

  public float? generationtimeMs {get; set;}
  public int? utcOffSetSeconds {get; set;}
  
  public string? timezoneAbbr {get; set;}
  public int? elevation {get; set;}
  
  public CurrentUnits? currentUnits {get; set;}
  public Current? current {get; set;}
  public HourlyUnits? hourlyUnits {get; set;}
  
  public Hourly? hourly {get; set;}
  public DailyUnits? dailyUnits {get; set;}
  public Daily? daily {get; set;}
  
  public List<double>? daylightDuration {get; set;}
  public List<double>? uvIndexMax {get; set;}
  public List<double>? precipitionProbabilityMax {get; set;}

  public class CurrentUnits {
    public string? time {get; set;}
    public string? interval {get; set;}
    public string? temperatureTwoM {get; set;}
    public string? isDay {get; set;}
    public string? precipitation {get; set;}
    public string? pressureMsl {get; set;}
    public string? surfacePressure {get; set;}
    public string? windSpeedTenM {get; set;}
    public char? windDirectionTenM {get; set;}
    public string? windGustsTenM {get; set;}
  }
  
  public class Current {
    public DateTime? time {get; set;}

    public int? interval {get; set;} 
    public int? windDirectionTenM {get; set;}
    
    public int? isDay {get; set;}
    public int? precipitation {get; set;}
    
    public double? pressureMsl {get; set;} 
    public double? surfacePressure {get; set;}
    
    public double? temperatureTwoM {get; set;}
    public double? windSpeedTenM {get; set;}
    public double windGustsTenM {get; set;}
  }
  
  public class HourlyUnits {
    public DateTime time {get; set;}
    
    public string? temperatureTwoM {get; set;}
    public string? pressureMsl {get; set;}
    public string? visibility {get; set;}
    
    public char? precipitationProbability {get; set;}
    public char? surfacePressure {get; set;}
    
    public char? relativeHumidityTwoM {get; set;}
    public char? cloudCover {get; set;}
  }
  
  public class Hourly {
    public List<DateTime>? time {get; set;}

    public List<int>? cloudCover {get; set;}
    public List<int>? precipitationProbability {get; set;}
    public List<int>? relativeHumidityTwoM {get; set;}
    
    public List<double>? pressureMsl {get; set;}
    public List<double>? surfacePressure {get; set;}
    public List<double>? temperatureTwoM {get; set;}
    public List<double>? visibility {get; set;}
  }
  
  public class DailyUnits {
    public string? time {get; set;}
    public string? sunrise {get; set;}
    public string? sunset {get; set;}

    public char? daylightDuration {get; set;}
    public char? precipitationProbabilityMax {get; set;}
  }
  
  public class Daily {
    public List<Date>? time {get; set;}
    public List<DateTime>? sunrise {get; set;}
    public List<DateTime>? sunset {get; set;}
  }
}
