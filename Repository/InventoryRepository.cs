using Dapper;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using UmniahPrizeWheel.Context;
using UmniahPrizeWheel.Models;
using UmniahPrizeWheel.Repository.Interface;

namespace UmniahPrizeWheel.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DapperContext _context;

        public InventoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task AddTransaction(int id)
        {
            var query = "INSERT INTO Transactions (ItemId) VALUES (@id)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<List<Inventory>> GetAll()
        {
            var query = "SELECT * FROM Inventory";
            using(var connection = _context.CreateConnection())
            {
                var inventory = await connection.QueryAsync<Inventory>(query);
                return inventory.ToList();
            }
        }

        public async Task<List<string>> GetItemNames()
        {
            var query = "SELECT Item FROM Inventory";
            using(var connection = _context.CreateConnection())
            {
                var items = await connection.QueryAsync<string>(query);
                return items.ToList();
            }
        }

        public async Task<int> GetQuantity()
        {
            var query = "SELECT SUM(Quantity) FROM Inventory";
            using(var connection = _context.CreateConnection())
            {
                int quantity = await connection.ExecuteScalarAsync<int>(query);
                return quantity;
            }
        }

        public async Task<int> GetUsed(int id)
        {
            var query = "SELECT COUNT(Id) FROM Transactions WHERE ItemId = @id";
            using (var connections = _context.CreateConnection())
            {
                int used = await connections.ExecuteScalarAsync<int>(query, new { id });
                return used;
            }
        }

    }
}
