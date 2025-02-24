using Microsoft.AspNetCore.SignalR;

namespace API_ApplicazioneCalendario.Services
{
    public class DataHub : Hub
    {
        public async Task SendUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
    }
}
