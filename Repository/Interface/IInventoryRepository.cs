using UmniahPrizeWheel.Models;

namespace UmniahPrizeWheel.Repository.Interface
{
    public interface IInventoryRepository
    {
        public Task<List<Inventory>> GetAll();
        public Task<List<string>> GetItemNames();
        public Task<int> GetQuantity();
        public Task<int> GetUsed(int id);
        public Task AddTransaction(int id);
    }
}
