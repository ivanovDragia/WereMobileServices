using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WereMobileServices.Simple
{
    public class DocumentRow
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string LotID { get; set; }
        public double Quantity { get; set; }
        public decimal Sum { get; set; }
        public string DocumentID { get; set; }

    }
}
