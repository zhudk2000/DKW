using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Domain.Business
{
    public class OrderDetail : Entity
    {
        public OrderDetail()
        {
            string Order_line_id = string.Empty;
            string Order_id = string.Empty;
            string Category_id = string.Empty;
            float Quantity = 0;
            string Qnit_uom = string.Empty;
            float Unit_quantity = 0;
            float Unit_price = 0;
            float Amount = 0;
            string Spec = string.Empty;
            string Service_item = string.Empty;
            float Storage = 0;
        }


        public string Order_line_id { get; set; }
        public string Order_id { get; set; }
        public string Category_id { get; set; }
        public float Quantity { get; set; }
        public string Qnit_uom { get; set; }
        public float Unit_quantity { get; set; }
        public float Unit_price { get; set; }
        public float Amount { get; set; }
        public string Spec { get; set; }
        public string Service_item { get; set; }
        public float Storage { get; set; }
    }
}
