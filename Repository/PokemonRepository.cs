using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository

    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }
        

        public ICollection<Pokemon> GetPokemons()
        {
            //_context.Pokemons để lấy call entity của pokemon sau đok chuyển tất cả record của nó ra một list
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }
    }
}

