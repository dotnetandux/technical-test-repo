using Company.AccountService.Api;
using Company.AccountService.Application.Config;
using Company.AccountService.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)
);

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<AccountFreezeEnforcer>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
