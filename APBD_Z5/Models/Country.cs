namespace APBD_Z5.Models;

public class Country
{
    public int IdCountry { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<CountryTrip> VCountryTrips { get; set; }
}