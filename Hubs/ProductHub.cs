using Microsoft.AspNetCore.SignalR;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.Models;
//using ThothSystemVersion1.Data; // Assuming you have a DbContext for your database

namespace ThothSystemVersion1.Hubs
{
    public class ProductHub : Hub
    {
        private readonly ThothContext _context; // Add your DbContext here

        public ProductHub(ThothContext context)
        {
            _context = context;
        }
        public override async Task OnConnectedAsync()
        {
            // Check reorder points when a client connects
            await CheckPaperReorderPoint();
            await CheckInkReorderPoint();
            await CheckSupplyReorderPoint();
            await base.OnConnectedAsync();
        }

        public async Task CheckPaperReorderPoint()
        {
            var papers = _context.Papers.ToList();
            foreach (var paper in papers)
            {
                if (paper.Quantity < paper.ReorderPoint)
                {
                    await Clients.All.SendAsync(
                        "ReceiveReorderMessagePaper",
                        $"Reorder point reached for paper: {paper.Name}"
                    );
                }
            }
        }


        // Method to check reorder point for Ink
        public async Task CheckInkReorderPoint()
        {
            var inks = _context.Inks.ToList();
            foreach (var ink in inks)
            {
                if (ink.Quantity < ink.ReorderPoint)
                {
                    await Clients.All.SendAsync("CheckInkReorderPointInk", $"Reorder point reached for ink: {ink.Name}");
                }
            }
        }

        // Method to check reorder point for Supply
        public async Task CheckSupplyReorderPoint()
        {
            var supplies = _context.Supplies.ToList();
            foreach (var supply in supplies)
            {
                if (supply.Quantity < supply.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessageSupply", $"Reorder point reached for supply: {supply.Name}");
                }
            }
        }
    }
}