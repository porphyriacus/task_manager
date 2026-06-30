using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using TaskManager.API.Hubs;

var builder = WebApplication.CreateBuilder(args);


var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(connectionstring));

// core
builder.Services.AddValidators()
                .AddServices();
// dal
builder.Services.AddRepositories();



var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new Exception("JWT SecretKey not configured");
var issuer = jwtSettings["Issuer"] ?? "TaskManagerAPI";
var audience = jwtSettings["Audience"] ?? "TaskManagerClient";
var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,        
            ValidateAudience = true,      

            ValidateLifetime = true, 
            ValidateIssuerSigningKey = true, // подпись 

            ValidIssuer = issuer,         // кто должен быть выпускающим
            ValidAudience = audience,     // для кого токен

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // ключ для проверки
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // токен в Query String для сигнала
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/taskHub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };

    });
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{


    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Manager API",
        Version = "v1",
        Description = "API for pupupu"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter tokem. Example: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    c.AddSecurityRequirement((document) => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});
//signal
builder.Services.AddSignalR();
//swager
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    //var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    //var testUser = await userManager.FindByIdAsync("test_user_id");
    //if (testUser == null)
    //{
    //    testUser = new User
    //    {
    //        Id = "test_user_id",
    //        UserName = "test_user",
    //        Email = "test@example.com",
    //        EmailConfirmed = true
    //    };
    //    await userManager.CreateAsync(testUser, "Test123!");
    //}
    //if (!context.Boards.Any())
    //{
    //    if (!context.Projects.Any())
    //    {
    //        var project = new Project(
    //            ownerId: "test_user_id",
    //            name: "Тестовый проект",
    //            description: "Для тестирования"
    //        );
    //        context.Projects.Add(project);
    //        context.SaveChanges(); 
    //    }

    //    var board = new Board(
    //        projectId: 1,
    //        name: "Тестовая доска",
    //        description: "Доска для тестирования задач"
    //    );
    //    context.Boards.Add(board);
    //    context.SaveChanges(); 
    //}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<TaskHub>("/taskHub");

app.Run();

