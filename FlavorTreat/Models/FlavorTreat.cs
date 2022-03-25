namespace FlavorTreat.Models
{
  public class FlavorTreat
  {       
    public int FlavorTreatId { get; set; }
    public int FlavorId { get; set; }
    public int TreatId { get; set; }
    public virtual Flavor flavor { get; set; }
    public virtual Treat treat { get; set; }
  }
}