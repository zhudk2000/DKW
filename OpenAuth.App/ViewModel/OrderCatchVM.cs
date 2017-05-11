using System;
using Infrastructure;
using OpenAuth.Domain;

namespace OpenAuth.App.ViewModel
{
    public class OrderCatchVM
    {
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

        public string Order_line_id { get; set; }

        public string Category_id { get; set; }

        public float Quantity { get; set; }

        public string Qnit_uom { get; set; }

        public float Unit_quantity { get; set; }

        public float Unit_price { get; set; }

        public float Amount { get; set; }

        public string Spec { get; set; }

        public string Service_item { get; set; }

        public float Storage { get; set; }

        public string User_ID { set; get; }
    }
}
