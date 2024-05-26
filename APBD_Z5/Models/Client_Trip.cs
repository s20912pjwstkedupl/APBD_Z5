namespace APBD_Z5.Models;

public class ClientTrip
{
    public int IdClient { get; set; }
    public int IdTrip { get; set; }
    public DateTime? RegisteredAt { get; set; }
    public DateTime PaymentDate { get; set; }
    
    public virtual Client VClient { get; set; }
    public virtual Trip VTrip { get; set; }
}