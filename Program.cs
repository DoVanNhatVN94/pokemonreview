using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Nơi chứa ứng dụng thưc thi
        // Add services to the container : Một nơi chứa các service được thêm vào

        builder.Services.AddControllers();

        // đăng ký service có tên là Seed với lifetime

//        Có ba loại lifetime được hỗ trợ trong ASP.NET Core:

//        Transient: mỗi lần được yêu cầu, container sẽ trả về một instance mới của đối tượng.
//          Scoped: container sẽ trả về cùng một instance của đối tượng cho tất cả các yêu cầu trong cùng một           scope(phạm vi). Khi scope bị xóa, đối tượng sẽ được giải phóng.
//          Singleton: container sẽ trả về cùng một instance của đối tượng cho tất cả các yêu cầu. Đối tượng sẽ được giữ trong bộ nhớ cho đến khi container bị giải phóng.
        builder.Services.AddTransient<Seed>();//Seed class được sử dụng để thực hiện seeding data (khởi tạo dữ liệu ban đầu) cho database của ứng dụng.

        // cần có DI của gói package automapper.extensions.Microsoft.DependencỵInection vì nó chưa định nghĩa được AddAutoMapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // ===================== REPOSITORY ======================
        // Add DI của repository vào service
        builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
        builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
        builder.Services.AddScoped<ICountryRepository, CountryRepository>();
        builder.Services.AddScoped<IOwnerRepository,OwnerRepository>();
        builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();// Thêm cấu hình chung gian swagger

        //AddDbContext<DataContext> là phương thức đăng ký service, nó được gọi trên đối tượng builder.Services.
        //cấu hình để sử dụng SQL Server với chuỗi kết nối lấy từ file appsettings.json
        builder.Services.AddDbContext<DataContext>(option =>{                  option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        var app = builder.Build();

        //Trong phương thức này, đầu tiên kiểm tra tham số dòng lệnh được truyền vào. Nếu tham số là "seeddata", thì ứng dụng sẽ gọi phương thức SeedData để khởi tạo dữ liệu.
        if (args.Length == 1 && args[0].ToLower() == "seeddata")
            SeedData(app);
        void SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            //Phương thức SeedData sử dụng IServiceScopeFactory để tạo ra một Scope để đảm bảo rằng các đối tượng được tạo ra trong Seed sẽ được giải phóng sau khi hoàn thành tác vụ của chúng
            using (var scope = scopedFactory.CreateScope())
            {
                //Sau đó, phương thức gọi phương thức SeedDataContext trong Seed để khởi tạo dữ liệu cho ứng dụng.
                var service = scope.ServiceProvider.GetService<Seed>();
                service.SeedDataContext();
            }
        }



        // Configure the HTTP request pipeline. : Thiết lập đường dẫn http request
        if (app.Environment.IsDevelopment())
        {
            //Kích hoạt phần mềm trung gian để phục vụ tài liệu JSON được tạo và giao diện người dùng Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.UseHttpsRedirection();// giúp chuyển hướng http

        app.UseAuthorization(); // giúp ủy quyền


        app.MapControllers(); // Applie các controller

        app.Run();
    }
}

