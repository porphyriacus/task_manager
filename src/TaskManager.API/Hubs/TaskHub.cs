using Microsoft.AspNetCore.SignalR;
using System.Linq.Expressions;
using System.Security.Claims;
namespace TaskManager.API.Hubs
{
    public class TaskHub : Hub
    {
        private readonly IProjectService _projectService;
        public TaskHub(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var projects = await _projectService.GetAllAsync(default, new List<Expression<Func<Project, bool>>> { p => p.OwnerId == userId});

            foreach (var project in projects)
            {
                await Groups.AddToGroupAsync(connectionId, $"project_{project.Id}");
            }
            await Groups.AddToGroupAsync(connectionId, $"user_{userId}");
             
            await base.OnConnectedAsync();
        }

        public async Task SendTaskUpdate(TaskSummaryDto dto)
        {
            await Clients.Groups($"project_{dto.ProjectId}").SendAsync("ReceiveUpdatedTask", dto);
        }

        public async Task SendTaskCreate(TaskSummaryDto dto)
        {
            await Clients.Groups($"project_{dto.ProjectId}").SendAsync("ReceiveNewTask", dto);
        }

    }

}
