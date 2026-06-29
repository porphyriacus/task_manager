using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
namespace TaskManager.API.Hubs
{
    public class TaskHub : Hub
    {
        private readonly IProjectRepository _projectRepository;
        public TaskHub(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var projects = await 

            return base.OnConnectedAsync();
        }
    }
}
