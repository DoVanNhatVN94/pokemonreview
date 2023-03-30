using System;
namespace PokemonReviewApp.DTO
{
    //môt cấu trúc chứa các thông tin cần thiết khi mapper => nó sẽ chỉ lấy các thông tin đc mapper giữ dto và entity
	public class PokemonDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

