using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UmniahPrizeWheel.Models
{
    public class Transactions
    {
        [Key]
        public int Id;
        public int ItemId;
    }
}
