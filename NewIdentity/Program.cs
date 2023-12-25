using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NewIdentity.Data;
using NewIdentity.Entity;
using NewIdentity.ExceptionHandler;
using NewIdentity.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services
    .AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddExceptionHandler<GlobalHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<MyDemoContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 7;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 3;
})
    .AddEntityFrameworkStores<MyDemoContext>()
    .AddApiEndpoints();

builder.Services.AddSingleton<IEmailSender<User>, MailSender>();

builder.Services.AddAuthorizationBuilder();
//builder.Services.AddAuthorization(options => options.AddPolicy("maxAge", AuthorizationPolicy.MaxAgePolicy));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseExceptionHandler(_ => { });
app.MapGroup("/Identity").MapIdentityApi<User>();
app.MapGet("/Exception", () =>
{
    throw new NotImplementedException();
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
