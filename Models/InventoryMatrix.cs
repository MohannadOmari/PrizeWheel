namespace UmniahPrizeWheel.Models
{
    public class InventoryMatrix
    {
        public int id;
        public string item;
        public int quantity;
        public int used;
        public int available;
        public int from;
        public int to;

        public InventoryMatrix(int id, string item, int quantity)
        {
            this.id = id;
            this.item = item;
            this.quantity = quantity;
            used = 0;
            available = 0;
            from = 0;
            to = 0;
        }

    }
}
