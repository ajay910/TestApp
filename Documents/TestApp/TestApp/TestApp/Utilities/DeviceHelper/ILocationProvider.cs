namespace Aglive.Business.Infrastructure.Utilities
{
    using System;
    using System.Threading.Tasks;

    public interface ILocationProvider
    {
        LocationPoint CurrentLocation { get; set; }
        bool CanAccessLocation { get; set; }

        event EventHandler OnLocationUpdate;

        Task<LocationAddress> GetCurrentLocationAddress();

        Task<string> GetCurrentLocationAddressInString();
    }

    public class LocationPoint
    {
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }

    public class LocationAddress
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
    }
}