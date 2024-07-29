namespace Weatherify.Models
{
  public class Daily {
    public int WeatherId {get; set;}
    public List<DateTime>? time {get; set;}
    public List<DateTime>? sunrise {get; set;}
    public List<DateTime>? sunset {get; set;}
  }
}
