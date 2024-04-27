using Ecommerce.Data;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.EmailModel;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.AddressRepository;
using Ecommerce.Repository.Repositories.CountaryRepository;
using Ecommerce.Repository.Repositories.OrderLineRepository;
using Ecommerce.Repository.Repositories.OrderStatusRepository;
using Ecommerce.Repository.Repositories.PaymentTypeRepository;
using Ecommerce.Repository.Repositories.ProductCategoryRepository;
using Ecommerce.Repository.Repositories.ProductImagesRepository;
using Ecommerce.Repository.Repositories.ProductItemRepository;
using Ecommerce.Repository.Repositories.ProductRepository;
using Ecommerce.Repository.Repositories.ProductVariationRepository;
using Ecommerce.Repository.Repositories.PromotionCategoryRepository;
using Ecommerce.Repository.Repositories.PromotionRepository;
using Ecommerce.Repository.Repositories.ShippingMethodRepository;
using Ecommerce.Repository.Repositories.ShopOrderRepository;
using Ecommerce.Repository.Repositories.ShoppingCartItemRepository;
using Ecommerce.Repository.Repositories.ShoppingCartRepository;
using Ecommerce.Repository.Repositories.UserAddressRepository;
using Ecommerce.Repository.Repositories.UserPaymentMethodRepository;
using Ecommerce.Repository.Repositories.UserReviewRepository;
using Ecommerce.Repository.Repositories.VariationOptionsRepository;
using Ecommerce.Repository.Repositories.VariationRepository;
using Ecommerce.Service.Services.AddressService;
using Ecommerce.Service.Services.CountaryService;
using Ecommerce.Service.Services.EmailService;
using Ecommerce.Service.Services.OrderLineService;
using Ecommerce.Service.Services.OrderStatusService;
using Ecommerce.Service.Services.PaymentTypeService;
using Ecommerce.Service.Services.ProductCategoryService;
using Ecommerce.Service.Services.ProductItemService;
using Ecommerce.Service.Services.ProductService.ProductService;
using Ecommerce.Service.Services.ProductVariationService;
using Ecommerce.Service.Services.PromotionCategoryService;
using Ecommerce.Service.Services.PromotionService;
using Ecommerce.Service.Services.ShippingMethodService;
using Ecommerce.Service.Services.ShopOrderService;
using Ecommerce.Service.Services.ShoppingCartItemService;
using Ecommerce.Service.Services.ShoppingCartService;
using Ecommerce.Service.Services.UserAddressService;
using Ecommerce.Service.Services.UserPaymentMethodService;
using Ecommerce.Service.Services.UserReviewService;
using Ecommerce.Service.Services.UserService;
using Ecommerce.Service.Services.VariationOptionService;
using Ecommerce.Service.Services.VariationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
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

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Connect to database
builder.Services.AddDbContext<ApplicationDbContext>(op =>
{
    op.UseSqlServer(connection);
}
);
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

builder.Services.AddHttpContextAccessor();

//Add Config for Required Email
builder.Services.Configure<IdentityOptions>(
    opts => 
    {
        opts.SignIn.RequireConfirmedEmail = true;
        opts.Password.RequiredLength = 8;
    }
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

// inject repositories
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
builder.Services.AddScoped<IUserAddress, UserAddressRepository>();
builder.Services.AddScoped<IPaymentType, PaymentTypeRepository>();
builder.Services.AddScoped<IUserPaymentMethod, UserPaymentMethodRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartItem, ShoppingCartItemRepository>();
builder.Services.AddScoped<IShippingMethod, ShippingMethodRepository>();
builder.Services.AddScoped<IOrderStatus, OrderStatusRepository>();
builder.Services.AddScoped<IShopOrder, ShopOrderRepository>();
builder.Services.AddScoped<IOrderLine, OrderLineRepository>();
builder.Services.AddScoped<IUserReview, UserReviewRepository>();
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
builder.Services.AddScoped<IUserAddressService, UserAddressService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IUserPaymentMethodService, UserPaymentMethodService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
builder.Services.AddScoped<IShippingMethodService, ShippingMethodService>();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
builder.Services.AddScoped<IShopOrderService, ShopOrderService>();
builder.Services.AddScoped<IOrderLineService, OrderLineService>();
builder.Services.AddScoped<IUserReviewService, UserReviewService>();

// to kill circular in json
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce APIs", Version = "v1" });
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


app.UseStatusCodePagesWithRedirects("/error/{0}");

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
});

app.MapSwagger().RequireAuthorization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
