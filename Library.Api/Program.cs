using Library.BL.Services;
using Library.Data;
using Library.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;

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
builder.Services.AddScoped<IStorageService>(_ => new StorageService(Environment.GetEnvironmentVariable("BlobConfiguration:StorageConnectionString")!));
builder.Services.AddScoped<IServiceBusService>(_ => new ServiceBusService(Environment.GetEnvironmentVariable("ServiceBusConnection")!));
builder.Services.AddScoped<IBookAuditService>(_ => new BookAuditService(builder.Configuration["FunctionConfiguration:BookAuditUrl"], Environment.GetEnvironmentVariable("FunctionKey")!));

builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "azure-diagnostics-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.Run();