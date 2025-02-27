using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;

namespace API_ApplicazioneCalendario.Services
{
    public class GruppoHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} si è unito al gruppo {groupName}");
        }

        public async Task AddImpegno(string groupId, Impegno nuovoImpegno)
        {


            // Invia il nuovo impegno a tutti nel gruppo
            await Clients.Group(groupId).SendAsync("ReceiveImpegnoUpdate", nuovoImpegno);
        }
        public async Task SendUpdateToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveUpdate", message);
        }
    }
}
