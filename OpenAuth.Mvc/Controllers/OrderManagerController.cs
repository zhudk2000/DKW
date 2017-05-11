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

        public ActionResult OrderCatch()
        {
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
    }
}