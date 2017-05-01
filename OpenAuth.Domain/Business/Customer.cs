using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Domain.Business
{
    public partial class Customer : Entity
    {
        public Customer()
        {
            this.Customer_ID = string.Empty;
            this.Customer_Name = string.Empty;
            this.Contacts = string.Empty;
            this.Contact_Tel = string.Empty;
            this.Contact_Mob = string.Empty;
            this.Customer_Addr = string.Empty;
            this.Contract_Num = string.Empty;
            this.Advance_Amt = 0;
            this.Credit = 0;
            this.Amount_Method = string.Empty;
            this.Payment_Method = string.Empty;
            this.Bank = string.Empty;
            this.Bank_Acct = string.Empty;
            this.Customer_Type = 0;

            this.User_Account = string.Empty;
            this.User_Name = string.Empty;
            this.User_Password = string.Empty;

        }

        public string Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Contacts { get; set; }
        public string Contact_Tel { get; set; }
        public string Contact_Mob { get; set; }
        public string Customer_Addr { get; set; }
        public string Contract_Num { get; set; }
        public double Advance_Amt { get; set; }
        public double Credit { get; set; }
        public string Amount_Method { get; set; }
        public string Payment_Method { get; set; }
        public string Bank { get; set; }
        public string Bank_Acct { get; set; }
        public int Customer_Type { get; set; }

        public string User_Account { get; set; }
        public string User_Password { get; set; }
        public string User_Name { get; set; }
        //Account, Password, Name
    }
}
