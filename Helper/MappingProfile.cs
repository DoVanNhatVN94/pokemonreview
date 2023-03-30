using System;
using AutoMapper;
using PokemonReviewApp.DTO;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    // Đây là lớp hỗ trợ cho các vấn đề liên quan đến DTO và Entity
    //Profile là một class của AutoMapper, một thư viện được sử dụng để thực hiện ánh xạ đối tượng tự động
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            //Tạo mapper giữa model và class dto
            //Tất cả các mapper sẽ ánh xạ khi gọi Imapper.Map<>
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CountryDto, Country>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<PokemonDto, Pokemon>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
	}
}

