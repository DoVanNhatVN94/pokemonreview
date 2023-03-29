using System;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
	public class DataContext: DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Owner> Owners { get; set; }
		public DbSet<Pokemon> Pokemons { get; set; }
		public DbSet<PokemonOwner> PokemonOwners { get; set; }
		public DbSet<PokemonCategory> PokemonCategories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Reviewer> Reviewers { get; set; }

        //OnModelCreating là một phương thức ảo (virtual) được định nghĩa trong lớp DbContext của Entity Framework Core. Phương thức này được ghi đè (override) để cấu hình các thông tin liên quan đến model của ứng dụng, bao gồm các thành phần như entity, relationship, primary key, index, và các mapping configuration giữa các thành phần.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Được sử dụng trong việc thiết lập khóa chính cho đối tượng PokemonCategory trong quá trình tạo các bảng trong cơ sở dữ liệu.

//            Dòng code này đang được sử dụng trong việc thiết lập khóa chính cho đối tượng PokemonCategory trong quá trình tạo các bảng trong cơ sở dữ liệu.

//				Trong đó, phương thức HasKey() được sử dụng để chỉ định rằng khóa chính của bảng là sự kết hợp của hai thuộc tính PokemonId và CategoryId của đối tượng PokemonCategory.
            modelBuilder.Entity<PokemonCategory>()
				.HasKey(pc => new {pc.PokemonId,pc.CategoryId});//set khóa chính là hai khóa phụ
			modelBuilder.Entity<PokemonCategory>()
				.HasOne(p => p.Pokemon) 
				.WithMany(pc => pc.PokemonCategories)
				.HasForeignKey(p => p.PokemonId);
			modelBuilder.Entity<PokemonCategory>()
				.HasOne(c => c.Category)
				.WithMany(pc => pc.PokemonCategories)
				.HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<PokemonOwner>()
               .HasKey(po => new { po.PokemonId, po.OwnerId });
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(o => o.Owner)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(o => o.OwnerId);

        }

    }
}

