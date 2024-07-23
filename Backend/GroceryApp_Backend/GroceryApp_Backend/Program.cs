using AutoMapper;
using BuisnessLayer.AutoMapper;
using BuisnessLayer.IRepository;
using BuisnessLayer.Repository;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GroceryConnection")));
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<IOrderRepo,OrderRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....")),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});
builder.Services.AddSingleton<IMapper>(MappingConfig.Configure());
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    SeedData(dbContext);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapControllers();

app.Run();
static void SeedData(AppDbContext dbContext)
{
    if (!dbContext.Users.Any(user => user.Email == "admin@gmail.com" && user.Password=="admin123@" && user.Role=="Admin"))
    {
        var users = new UserEntity
        {
            FullName="Admin",
            Email="admin@gmail.com",
            Password="admin123@",
            Role="Admin",
            PhoneNumber=8303439859
        };
        dbContext.Users.Add(users);
        dbContext.SaveChanges();
    }
}