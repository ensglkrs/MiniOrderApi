using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniOrderApi.Data;
using MiniOrderApi.DTOs.Customer;
using MiniOrderApi.DTOs.Order;
using MiniOrderApi.DTOs.Product;
using MiniOrderApi.Repositories.EntityFramework;
using MiniOrderApi.Repositories.Interfaces;
using MiniOrderApi.Services;
using MiniOrderApi.Services.Interfaces;
using MiniOrderApi.Validators;
using System.Text;
using AM = AutoMapper;
using FV = FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// 1. Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Controllers & Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<MiniOrderApi.Filters.ValidationFilter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// 3. Swagger (JWT Support)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

// 4. JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]!; // "!" ensures it's not null

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// 5. AutoMapper
var mapConfig = new AM.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MiniOrderApi.Mappings.MappingProfile());
});
builder.Services.AddSingleton(mapConfig.CreateMapper());

// 6. Repositories
builder.Services.AddScoped<ICustomerRepository, EfCustomerRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

// 7. Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDashboardService, DashboardService>(); // <--- WEEK 14 EKLENTÝSÝ BURADA!

// 8. Validators
builder.Services.AddScoped<FV.IValidator<CreateCustomerRequest>, CreateCustomerValidator>();
builder.Services.AddScoped<FV.IValidator<CreateProductRequest>, CreateProductValidator>();
builder.Services.AddScoped<FV.IValidator<CreateOrderRequest>, CreateOrderValidator>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Auth Middleware (Order is important)
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<MiniOrderApi.Middlewares.GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();