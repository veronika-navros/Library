using Library.BL.Services;
using Library.Data;
using Library.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection")));

builder.Services.AddScoped<ILibraryContext, LibraryContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IStorageService>(_ => new StorageService(Environment.GetEnvironmentVariable("BlobStorageConnection")!));
builder.Services.AddScoped<IServiceBusService>(_ => new ServiceBusService(Environment.GetEnvironmentVariable("ServiceBusConnection")!));
builder.Services.AddScoped<IBookAuditService>(_ => new BookAuditService(builder.Configuration["FunctionConfiguration:BookAuditUrl"], Environment.GetEnvironmentVariable("FunctionKey")!));

// builder.Logging.AddAzureWebAppDiagnostics();
// builder.Services.Configure<AzureBlobLoggerOptions>(options =>
// {
//     options.FileNameFormat = _ => "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level}] {RequestId}-{SourceContext}: {Message}{NewLine}{Exception}";
//     options.BlobName = "$logs";
// });

Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
        .Enrich.FromLogContext()
        .WriteTo.AzureAnalytics("47416c81-85ac-480d-a677-daebcbbb740a", "Win/N7KmxWqiDg/3ksb1yOLAN+xLlcbhFd8iIeuonuMBkUobqDnZZFoEBuJo5PeifdZQhW14hppi4RIeKSBVBg==", "veron-library")
        .WriteTo.Console()
        .CreateLogger();
builder.Logging.AddSerilog();
builder.Host.UseSerilog();
// builder.Host.UseSerilog((ctx, lc) => lc
//         .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();
app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.Run();