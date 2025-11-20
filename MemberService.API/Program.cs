using System.Text.Json.Serialization;
using MemberService.API.Configuration;
using MemberService.API.Middlewares;
using MemberService.BO.Common;
using Net.payOS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var PayOS = builder.Configuration.GetSection("PAYOS");
var ClientId = Environment.GetEnvironmentVariable("CLIENT_ID") ?? PayOS["CLIENT_ID"];
var APILEY = Environment.GetEnvironmentVariable("API_KEY") ?? PayOS["API_KEY"];
var CHECKSUMKEY = Environment.GetEnvironmentVariable("CHECKSUM_KEY") ?? PayOS["CHECKSUM_KEY"];

PayOS payOS = new PayOS(ClientId,
                    APILEY,
                    CHECKSUMKEY);
                    
builder.Services.AddSingleton(payOS);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAppDI();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowExpoApp",
        policy =>
            policy
                //.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true)
    );
});

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseAppExceptionHandler();
app.UseCors("AllowExpoApp");
app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
