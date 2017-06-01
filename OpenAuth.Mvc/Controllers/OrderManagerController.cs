using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Infrastructure;
using LeaRun.Util.WebControl;
using OpenAuth.App;
using OpenAuth.App.Business;
using OpenAuth.App.ViewModel;
using OpenAuth.Domain;
using OpenAuth.Domain.Business;
using OpenAuth.Mvc.Models;
using OpenAuth.App.SSO;
using System.Text;

namespace OpenAuth.Mvc.Controllers
{
    public class OrderManagerController : BaseController
    {
        private OrderCatchApp _app;
        public OrderManagerController()
        {
            _app = AutofacExt.GetFromFac<OrderCatchApp>();
        }

        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Abc(OrderHeader view)
        {
            return _app.Abc(view);
        }

        [Authenticate]
        public ActionResult OrderCatch()
        {
            return View();
        }

        [Authenticate]
        public ActionResult OrderApprove()
        {
            string result = "approve";
            if (Request["act"] != null)
                result = "boss_approve";

            if (CurrentModule != null)
            {
                ViewData["approve_action"] = result;
            }
            return View();
        }

        [Authenticate]
        public ActionResult MyOrders()
        {
            string result = "";
            if (CurrentModule != null)
            {
                User usr = AuthUtil.GetCurrentUser().User;
                if (usr != null)
                {
                    string tmp = _app.GetCustID_NameByUserAcct(usr.Account);
                    if (tmp != "")
                    {
                        result = tmp.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                }
                
                ViewData["CustomerID"] = result;
            }
            return View();
        }

        [HttpPost]
        public string GetUserInfo()
        {
            Infrastructure.Response result = new Infrastructure.Response();
            User usr = AuthUtil.GetCurrentUser().User;
            try
            {
                result.Message = usr.Account;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(result);

        }

        [HttpPost]
        public String GetCustID_NameByUserAcct(string acct)
        {
            Infrastructure.Response result = new Infrastructure.Response();
            try
            {
                result.Message = _app.GetCustID_NameByUserAcct(acct);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(result);
        }

        [HttpPost]
        public string GetNextOrderNumber()
        {
            Infrastructure.Response result = new Infrastructure.Response();
            try
            {
                result.Message = _app.GetNextOrderNumber();
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(result);
        }

        [HttpPost]
        public string SaveOrderCatch(OrderCatchVM view)
        {
            Infrastructure.Response result = new Infrastructure.Response();
            try
            {
                _app.SaveOrderCatch(view);
                result.Message = "保存成功！";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(result);
        }

        [HttpPost]
        public string UpdateOrder(OrderCatchVM view)
        {
            Infrastructure.Response result = new Infrastructure.Response();
            try
            {
                _app.Update(view);
                result.Message = "保存成功！";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(result);
        }

        public string Load(int page = 1, int rows = 30, string cid = "")
        {
            return JsonHelper.Instance.Serialize(_app.Load(page, rows, cid));
        }

        public string Query(string dteFrom, string dteTo, string ordNO, string cnm, string ordStatus, int page = 1, int rows = 30, string cid = "")
        {
            return JsonHelper.Instance.Serialize(_app.Load(dteFrom, dteTo, ordNO, cnm, ordStatus, page, rows, cid));
        }

        public string UpdateOrderStatus(string ordID, string statusTo, string remark = "")
        {
            Infrastructure.Response result = new Infrastructure.Response();
            try
            {
                _app.UpdateOrderStatus(ordID, statusTo, remark);
                result.Message = "操作成功！";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(result);
        }
        
        public string DeleteOrder(string ordID)
        {
            Infrastructure.Response result = new Infrastructure.Response();
            try
            {
                _app.DeleteOrder(ordID);
                result.Message = "删除成功！";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(result);
        }
        //[ChildActionOnly]
        //public ActionResult GetOrderApproveAction()
        //{
        //    string result = "approve";
        //    if (Request[""] == "2")
        //        result = "boss_approve";

        //    var sb = new StringBuilder();
        //    if (CurrentModule != null)
        //    {
        //        //sb.Append("<input type=\"hidden\" id=\"hidAction\" value=\"").Append(result).Append("\" ");
        //        ViewData["approve_action"] = result;
        //    }

        //    return View();
        //}

    }
}