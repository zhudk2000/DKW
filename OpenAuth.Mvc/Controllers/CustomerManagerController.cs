using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Infrastructure;
using LeaRun.Util.WebControl;
using OpenAuth.App;
using OpenAuth.App.ViewModel;
using OpenAuth.Domain;
using OpenAuth.Mvc.Models;

namespace OpenAuth.Mvc.Controllers
{
    public class CustomerManagerController : BaseController
    {
        private CustomerApp _app;
        public CustomerManagerController()
        {
            _app = AutofacExt.GetFromFac<CustomerApp>();
        }

        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Add(CustomerInfo view)
        {
            try
            {
                _app.Add(view);

            }
            catch (Exception ex)
            {
                Result.Status = false;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        public string Update(CustomerInfo view)
        {
            try
            {
                _app.Update(view);
                Result.Message = "修改成功！";

            }
            catch (Exception ex)
            {
                Result.Status = false;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        public string Delete(String[] ids)
        {
            try
            {
                _app.Delete(ids);
                Result.Message = "操作成功！";
            }
            catch (Exception e)
            {
                Result.Status = false;
                Result.Message = e.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }

        public string Load(int page = 1, int rows = 30)
        {
            return JsonHelper.Instance.Serialize(_app.LoadCustomerInfo(page, rows));
        }

        public string Query(string ccd, string cnm, int page = 1, int rows = 30)
        {
            return JsonHelper.Instance.Serialize(_app.LoadCustomerInfo(ccd, cnm, page, rows));
        }
    }
}