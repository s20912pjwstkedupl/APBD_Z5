namespace APBD_Z5.Models;

public class CountryTrip
{
    public int IdCountry { get; set; }
    public int IdTrip { get; set; }
    
    public virtual Country VCountry { get; set; }
    public virtual Trip VTrip { get; set; }
}