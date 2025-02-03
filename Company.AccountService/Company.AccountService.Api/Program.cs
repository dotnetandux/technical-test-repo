using Company.AccountService.Api;
using Company.AccountService.Application.Config;
using Company.AccountService.Application.Handlers.Command;
using Company.AccountService.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateAccountHandler).Assembly)
);

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseWhen(context => ApplyMiddleware(context), appBuilder =>
{
    appBuilder.UseMiddleware<AccountFreezeEnforcer>();
});

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

bool ApplyMiddleware(HttpContext context)
{
    var request = context.Request;
    var routeValues = context.GetRouteData()?.Values;

    string? controller = routeValues?["controller"]?.ToString();
    string? action = routeValues?["action"]?.ToString();

    if (controller == "Accounts" && action == "CreateAccount")
        return false;

    if (controller == "Accounts" && (action == "UnfreezeAccount" || action == "FreezeAccount"))
        return false;

    return routeValues?.ContainsKey("id") == true;
}