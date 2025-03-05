using Microsoft.AspNetCore.SignalR;
using ThothSystemVersion1.Models;
//using ThothSystemVersion1.Data;
using ThothSystemVersion1.Database; // Assuming you have a DbContext for your database

namespace ThothSystemVersion1.Hubs
{
    public class ProductHub : Hub
    {
        private readonly ThothContext _context; // Add your DbContext here

        public ProductHub(ThothContext context)
        {
            _context = context;
        }

        // Method to check reorder point for Paper
        public async Task CheckPaperReorderPoint()
        {
            var papersList = _context.Papers.ToList();
            foreach (Paper paper in papersList)
            {
                if (paper.Quantity < paper.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessage", $"Reorder point reached for paper: {paper.Name}");
                }
            }
        }

        // Method to check reorder point for Ink
        public async Task CheckInkReorderPoint()
        {
            var inksList = _context.Inks.ToList();
            foreach (Ink ink in inksList)
            {
                if (ink.Quantity < ink.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessage", $"Reorder point reached for ink: {ink.Name}");
                }
            }
        }

        // Method to check reorder point for Supply
        public async Task CheckSupplyReorderPoint()
        {
            var suppliesList = _context.Supplies.ToList();
            foreach (Supply supply in suppliesList)
            {
                if (supply.Quantity < supply.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessage", $"Reorder point reached for supply: {supply.Name}");
                }
            }
        }
    }
}