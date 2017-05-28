using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Domain.Business
{
    public class OrderHeader : Entity
    {
        public OrderHeader()
        {
            List<OrderDetail> orderDetail = new List<OrderDetail>();
            string Order_id = string.Empty;
            string Customer_id = string.Empty;
            string Customer_name = string.Empty;
            string Contacts = string.Empty;
            string Contact_tel = string.Empty;
            string Contact_address = string.Empty;
            DateTime Order_date = DateTime.Now;
            string Contract_id = string.Empty;
            string Sales_name = string.Empty;
            DateTime Deliver_date;
            DateTime Pick_date;
            string Order_status = string.Empty;
            string Ar_Status = string.Empty;
            string Remark = string.Empty;

            float AvgUnitPrice = 0;
            float Amount = 0;
            int Quantity = 0;
        }

        public List<OrderDetail> orderDetail { get; set; }
        public string Order_id { get; set; }
        public string Customer_id { get; set; }
        public string Customer_name { get; set; }
        public string Contacts { get; set; }
        public string Contact_tel { get; set; }
        public string Contact_address { get; set; }
        public DateTime Order_date { get; set; }
        public string Contract_id { get; set; }
        public string Sales_name { get; set; }
        public DateTime Deliver_date { get; set; }
        public DateTime Pick_date { get; set; }
        public string Order_status { get; set; }
        public string Ar_Status { get; set; }
        public string Remark { get; set; }

        public float Amount { get; set; }
        public int Quantity { get; set; }
        public float Unit_price { get; set; }
    }
    
}
