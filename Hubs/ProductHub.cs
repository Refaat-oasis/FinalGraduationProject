using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ThothSystemVersion1.Hubs
{
    public class ProductHub : Hub
    {
        //Remote procedures calls set of methods

        public void ReorderMessage(string message) { 
            
            Clients.All.SendAsync(message);


        }



    }
}
