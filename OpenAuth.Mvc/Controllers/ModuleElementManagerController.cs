﻿// ***********************************************************************
// Assembly         : OpenAuth.Mvc
// Author           : Yubao Li
// Created          : 12-02-2015
//
// Last Modified By : Yubao Li
// Last Modified On : 12-02-2015
// ***********************************************************************
// <copyright file="ModuleElementManagerController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>模块元素管理，无需权限控制</summary>
// ***********************************************************************

using Infrastructure;
using OpenAuth.App;
using OpenAuth.Domain;
using OpenAuth.Mvc.Models;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace OpenAuth.Mvc.Controllers
{
    public class ModuleElementManagerController : BaseController
    {
        private ModuleElementManagerApp _app;

        public ModuleElementManagerController()
        {
            _app = AutofacExt.GetFromFac<ModuleElementManagerApp>();
        }
        public ActionResult Index(Guid id)
        {
            ViewBag.ModuleId = id;
            return View();
        }
        public ActionResult Get(Guid moduleId)
        {
            return Json(_app.LoadByModuleId(moduleId), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string AddOrEditButton(ModuleElement button)
        {
            try
            {
                _app.AddOrUpdate(button);
            }
            catch (DbEntityValidationException e)
            {
                
                 Result.Status=false;
                 Result.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize( Result);
        }
        public string Del(Guid[] ids)
        {
            try
            {
                _app.Delete(ids);
            }
            catch (Exception e)
            {
                 Result.Status=false;
                 Result.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize( Result);
        }

        /// <summary>
        /// 分配模块菜单（按钮）界面
        /// <para>可以为用户/角色分配，同过key（UserElement/RoleElement）区分</para>
        /// </summary>
        /// <param name="firstId">The first identifier.</param>
        /// <param name="key">The key.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult AssignModuleElement(Guid firstId, string key)
        {
            ViewBag.FirstId = firstId;
            ViewBag.ModuleType = key;
            return View();
        }
        public string LoadWithAccess(Guid tId, Guid firstId, string key)
        {
            return JsonHelper.Instance.Serialize(_app.LoadWithAccess(key, firstId, tId));
        }
    }
}