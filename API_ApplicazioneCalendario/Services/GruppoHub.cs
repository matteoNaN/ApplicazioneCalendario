using Microsoft.AspNetCore.SignalR;

namespace API_ApplicazioneCalendario.Services
{
    public class GruppoHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} si è unito al gruppo {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} ha lasciato il gruppo {groupName}");
        }

        public async Task SendUpdateToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveUpdate", message);
        }
    }
}
