using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Hubs;


var builder = WebApplication.CreateBuilder(args);
//swager
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(connectionstring));

// core
builder.Services.AddValidators()
                .AddServices();
// dal
builder.Services.AddRepositories();

//signal
builder.Services.AddSignalR();

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

app.UseRouting();
app.UseStaticFiles();
app.MapControllers();
app.MapHub<TaskHub>("/taskHub");

app.Run();

