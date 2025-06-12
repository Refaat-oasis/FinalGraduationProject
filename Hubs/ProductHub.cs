using Microsoft.AspNetCore.SignalR;
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

        // Method to check reorder point for Paper
        public async Task CheckPaperReorderPoint()
        {
            var papers = _context.Papers.ToList();
            foreach (var paper in papers)
            {
                if (paper.Quantity < paper.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessagePaper", $"تم الوصول للحد الادني من : {paper.Name} , \n بكمية :{paper.Quantity}");
                }
            }
        }

        // Method to check reorder point for Ink
        public async Task CheckInkReorderPoint()
        {
            var inks = _context.Inks.ToList();
            foreach (var ink in inks)
            {
                if (ink.NumberOfUnits < ink.ReorderPoint)
                {
                    await Clients.All.SendAsync("ReceiveReorderMessageInk", $"تم الوصول للحد الادني من : {ink.Name} ,\n بكمية :{ink.NumberOfUnits}");
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
                    await Clients.All.SendAsync("ReceiveReorderMessageSupply", $"تم الوصول للحد الادني من : {supply.Name} ,\n بكمية : {supply.Quantity}");
                }
            }
        }
    }
}