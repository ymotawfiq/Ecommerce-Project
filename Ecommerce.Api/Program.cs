using Ecommerce.Data;
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
using Ecommerce.Service.Services.ProductCategoryService;
using Ecommerce.Service.Services.ProductItemService;
using Ecommerce.Service.Services.ProductService;
using Ecommerce.Service.Services.ProductService.ProductService;
using Ecommerce.Service.Services.ProductVariationService;
using Ecommerce.Service.Services.PromotionCategoryService;
using Ecommerce.Service.Services.PromotionService;
using Ecommerce.Service.Services.VariationOptionService;
using Ecommerce.Service.Services.VariationService;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Connect to database
builder.Services.AddDbContext<ApplicationDbContext>(op =>

    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Transient);

// Repositories Injection
builder.Services.AddTransient<IProductCategory, ProductCategoryRepository>();
builder.Services.AddTransient<IProduct, ProductRepository>();
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
builder.Services.AddTransient<IProductCategoryService, ProductCategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<IProductItemService, ProductItemService>();
builder.Services.AddScoped<IVariationService, VariationService>();
builder.Services.AddScoped<IVariationOptionsService, VariationOptionsService>();
builder.Services.AddScoped<IProductVariationService, ProductVariationService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IPromotionCategoryService, PromotionCategoryService>();
builder.Services.AddScoped<ICountaryService, CountaryService>();
builder.Services.AddScoped<IAddressService, AddressService>();


// to kill circular in json
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
