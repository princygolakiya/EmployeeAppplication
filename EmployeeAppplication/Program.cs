using EmployeeAppplication.Configurations;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlServer(builder.Configuration);
builder.Services.AddAuthentications(builder.Configuration);
builder.Services.AddDependancy(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("oauth", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        BearerFormat = "Bearer",
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement(){
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference(){
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth"
                }
            },
            new string[] {}
        }
    });
});

//log
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
