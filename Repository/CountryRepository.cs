using System;

using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CountryRepository : ICountryRepository
	{
        private readonly DataContext _context;
     
		public CountryRepository(DataContext context)
		{
            _context = context;

		}

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(ct => ct.Id==id);
        }

        public bool CreateCountry(Country country)
        {
          
                _context.Add(country);
                return Saved();
            
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Saved();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(ct => ct.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(ct => ct.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool Saved()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Saved();
        }
    }
}

