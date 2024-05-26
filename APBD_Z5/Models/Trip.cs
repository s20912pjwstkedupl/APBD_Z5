namespace APBD_Z5.Models;

public class Trip
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    
    public virtual ICollection<ClientTrip> VClientTrips { get; set; }
    public virtual ICollection<CountryTrip> VCountryTrips { get; set; }
}