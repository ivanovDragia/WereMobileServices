using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WereMobileServices.Simple
{
    public class Lot
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public double Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LotNumber { get; set; }
        
    }
}
