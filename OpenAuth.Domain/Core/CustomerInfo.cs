using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Domain
{
    public partial class CustomerInfo :Entity
    {
        public CustomerInfo()
        {
            this.CustomerID = Guid.NewGuid().ToString();
            this.CustomerCode = String.Empty;
            this.CustomerName = string.Empty;
            this.CustomerAddr = string.Empty;
            this.CustomerType = string.Empty;
        }

        public string CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddr { get; set; }
        public string CustomerType { get; set; }
    }
}
