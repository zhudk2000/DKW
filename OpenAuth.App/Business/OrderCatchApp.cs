using OpenAuth.App.ViewModel;
using OpenAuth.Domain;
using OpenAuth.Domain.Interface;
using OpenAuth.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.Repository;
using OpenAuth.Repository.Business;

namespace OpenAuth.App.Business
{
    public class OrderCatchApp
    {
        private RepositoryOrderCatch _repo = new RepositoryOrderCatch();

        public string Abc(OrderHeader ord)
        {
            return _repo.abc();
        }

        public Customer GetCustomerByUserAcct(string acct)
        {
            BusinessUtility utl = new BusinessUtility();
            return utl.GetCustomerByLoginUserAcct(acct);
        }

        public String GetCustID_NameByUserAcct(string acct)
        {
            string result = "";
            Customer cust = GetCustomerByUserAcct(acct);
            if (cust != null)
                result = cust.Customer_ID + ";" + cust.Customer_Name;

            return result;
        }

        public string GetNextOrderNumber()
        {
            return _repo.GetNextOrderNumber();
        }

        //
        public void SaveOrderCatch(OrderCatchVM view)
        {
            OrderHeader oh = new OrderHeader();
            OrderDetail od = new OrderDetail();

            oh.Contacts = view.Contacts;
            oh.Contact_address = view.Contact_address;
            oh.Contact_tel = view.Contact_tel;
            oh.Contract_id = view.Contract_id;
            oh.Customer_id = view.Customer_id;
            oh.Customer_name = view.Customer_name;
            oh.Order_date = DateTime.Now;
            oh.Order_id = view.Order_id;
            oh.Sales_name = view.User_ID;

            od.Order_line_id = "1";
            od.Qnit_uom = "箱";
            od.Quantity = view.Quantity;
            od.Service_item = view.Service_item;
            od.Unit_price = view.Unit_price;
            od.Unit_quantity = view.Unit_quantity;
            od.Amount = view.Amount;

            oh.orderDetail = new List<OrderDetail>();
            oh.orderDetail.Add(od);

            _repo.SaveOrderCatch(oh);
        }

        public void Update(OrderCatchVM view)
        {
            OrderHeader oh = new OrderHeader();
            OrderDetail od = new OrderDetail();

            oh.Contacts = view.Contacts;
            oh.Contact_address = view.Contact_address;
            oh.Contact_tel = view.Contact_tel;
            oh.Contract_id = view.Contract_id;
            oh.Customer_id = view.Customer_id;
            oh.Customer_name = view.Customer_name;
            oh.Order_date = DateTime.Now;
            oh.Order_id = view.Order_id;
            oh.Sales_name = view.User_ID;

            od.Order_line_id = "1";
            od.Qnit_uom = "箱";
            od.Quantity = view.Quantity;
            od.Service_item = view.Service_item;
            od.Unit_price = view.Unit_price;
            od.Unit_quantity = view.Unit_quantity;
            od.Amount = view.Amount;

            oh.orderDetail = new List<OrderDetail>();
            oh.orderDetail.Add(od);

            _repo.Update(oh);
        }

        public GridData Load(int pageindex, int pagesize, string cid = "")
        {
            if (pageindex < 1) pageindex = 1;  //TODO:如果列表为空新增加一个用户后，前端会传一个0过来，奇怪？？
            IEnumerable<OrderHeader> ords;
            int records = _repo.GetCount(cid);

            ords = _repo.LoadOrder(pageindex, pagesize, cid);

            return new GridData
            {
                records = records,
                total = (int)Math.Ceiling((double)records / pagesize),
                rows = ords,
                page = pageindex
            };
        }

        public GridData Load(string dteFrom, string dteTo, string ordNO, string cnm, string ordStatus, int pageindex = 1, int pagesize = 30, string cid = "")
        {
            if (pageindex < 1) pageindex = 1;  //TODO:如果列表为空新增加一个用户后，前端会传一个0过来，奇怪？？
            IEnumerable<OrderHeader> ords;
            int records = _repo.GetCount(dteFrom, dteTo, ordNO, cnm, ordStatus, pageindex, pagesize, cid);

            ords = _repo.LoadOrder(dteFrom, dteTo, ordNO, cnm, ordStatus, pageindex, pagesize, cid);

            return new GridData
            {
                records = records,
                total = (int)Math.Ceiling((double)records / pagesize),
                rows = ords,
                page = pageindex
            };
        }

        public void UpdateOrderStatus(string ordID, string statusTo)
        {
            string userid = OpenAuth.App.SSO.AuthUtil.GetUserName();
            _repo.UpdateOrderStatus(ordID, statusTo, userid);
        }

        public void DeleteOrder(string ordID)
        {
            _repo.DeleteOrder(ordID);
        }
    }
}
