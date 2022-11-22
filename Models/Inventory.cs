using System.ComponentModel.DataAnnotations;

namespace UmniahPrizeWheel.Models
{
    public class Inventory
    {
        [Key]
        public int Id;
        public string Item;
        public int Quantity;
    }
}
