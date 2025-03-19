using Microsoft.AspNetCore.SignalR;

namespace NMS.Services
{
    public class SignalRService : Hub
    {
        public async Task SendNewsUpdate()
        {
            await Clients.All.SendAsync("ReceiveNewsUpdate");
        }
    }
}
