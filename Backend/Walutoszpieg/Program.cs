using Walutoszpieg.DAL;
using Walutoszpieg.Repositories;

namespace Walutoszpieg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<AlertRepository>();
            builder.Services.AddScoped<CurrencyRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<ExchangeRateRepository>();
            builder.Services.AddScoped<HistoricalExchangeRateRepository>();
            builder.Services.AddScoped<NotificationRepository>();
            builder.Services.AddScoped<TransactionHistoryRepository>();
            builder.Services.AddScoped<WalletRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
