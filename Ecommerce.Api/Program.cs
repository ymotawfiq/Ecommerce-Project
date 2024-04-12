using Ecommerce.Data;
using Ecommerce.Data.Models.EmailModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.AddressRepository;
using Ecommerce.Repository.Repositories.CountaryRepository;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Repository.Repositories.ProductVariationRepository;
using Ecommerce.Repository.Repositories.PromotionCategoryRepository;
using Ecommerce.Repository.Repositories.PromotionRepository;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Ecommerce.Service.Services.AddressService;
using Ecommerce.Service.Services.CountaryService;
using Ecommerce.Service.Services.EmailService;
using Ecommerce.Service.Services.ProductCategoryService;
using Ecommerce.Service.Services.ProductItemService;
using Ecommerce.Service.Services.ProductService.ProductService;
using Ecommerce.Service.Services.ProductVariationService;
using Ecommerce.Service.Services.PromotionCategoryService;
using Ecommerce.Service.Services.PromotionService;
using Ecommerce.Service.Services.UserService;
using Ecommerce.Service.Services.VariationOptionService;
using Ecommerce.Service.Services.VariationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Connect to database
builder.Services.AddDbContext<ApplicationDbContext>(op =>

    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Transient);
builder.Services.AddIdentity<SiteUser, IdentityRole>(options =>
{
    // Other identity options
    options.Tokens.ProviderMap["Email"] = new TokenProviderDescriptor(typeof(EmailTokenProvider<SiteUser>));
}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    // Set the expiration time for the OTP
    options.TokenLifespan = TimeSpan.FromMinutes(5); // Adjust the time span as needed
});


//Add Config for Required Email
builder.Services.Configure<IdentityOptions>(
    opts => opts.SignIn.RequireConfirmedEmail = true
    );

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});


//Add Email Configs
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

// Add services to the container.

builder.Services.AddControllers();

// Repositories Injection
builder.Services.AddScoped<IProductCategory, ProductCategoryRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IProductImages, ProductImagesRepository>();
builder.Services.AddScoped<IProductItem, ProductItemRepository>();
builder.Services.AddScoped<IVariation, VariationRepository>();
builder.Services.AddScoped<IVariationOptions, VariationOptionsRepository>();
builder.Services.AddScoped<IProductVariation, ProductVariationRepository>();
builder.Services.AddScoped<IPromotion, PromotionRepository>();
builder.Services.AddScoped<IPromotionCategory, PromotionCategoryRepository>();
builder.Services.AddScoped<ICountary, CountaryRepository>();
builder.Services.AddScoped<IAddress, AddressRepository>();
// inject services
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductItemService, ProductItemService>();
builder.Services.AddScoped<IVariationService, VariationService>();
builder.Services.AddScoped<IVariationOptionsService, VariationOptionsService>();
builder.Services.AddScoped<IProductVariationService, ProductVariationService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IPromotionCategoryService, PromotionCategoryService>();
builder.Services.AddScoped<ICountaryService, CountaryService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<IEmailService, EmailService>();


//// to kill circular in json
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(policy =>
{
    policy.WithOrigins(builder.Configuration["ClentSideUrl"]?? "https://localhost:7206")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType);
});


app.MapSwagger().RequireAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
