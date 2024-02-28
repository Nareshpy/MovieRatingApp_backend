using Microsoft.EntityFrameworkCore;
using MovieRatingApp.Data;
using MovieRatingApp.Interfaces;
using MovieRatingApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ReviewerRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IMovie, MovieRepository>();
builder.Services.AddScoped<IReview, ReviewRepository>();
builder.Services.AddScoped<IReviewer, ReviewerRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
  
        options.AddPolicy("_myAllowSpecificOrigins", builder =>
        {
            builder
                .WithOrigins("http://localhost:3000") // Replace with your allowed origin(s)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    
});
builder.Services.AddDbContext<DataContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
