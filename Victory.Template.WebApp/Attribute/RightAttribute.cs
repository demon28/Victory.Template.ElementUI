using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victory.Template.DataAccess.CodeGenerator;
using Victory.Template.Entity.CodeGenerator;
using Victory.Template.Entity.Enums;
using Victory.Template.Entity;

namespace Victory.Template.WebApi.Attribute
{
    public class RightAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 忽略权限
        /// </summary>
        public bool Ignore { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PowerName { get; set; }


        public override void OnActionExecuting(ActionExecutingContext Context)
        {
            base.OnActionExecuting(Context);


            //先取出登录用户id
            int userid = int.Parse(Context.HttpContext.User.FindFirst("userId").Value);


            //根据配置文件决定是否给初次登录的用户 分配一个默认的登录角色
            
            if (AppConfig.IsSetDefautlRole)
            {
                SetDefaultRole(userid);

            }


            //如果Ignore 为true 则表示不检查该操作，这里只给他初次登录分配 普通会员角色
            if (Ignore)
            {
                return;
            }


            //获取路由地址

            string areaName = string.Empty;
            string controllerName = string.Empty;
            string actionName = string.Empty;

            string page = GetPageUrl(Context, ref areaName, ref controllerName, ref actionName);



            //判断请求的 为访问页面 还是 请求功能操作 Ajax请求为功能， 非ajax请求为访问页面
            var isAjax = Context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";


            //判断数据库是否存在该权限，不存则自动添加，无需手动配置
            AddActionFunc(controllerName, actionName, areaName, page, isAjax);


            //如果全局配置忽略权限，则忽略检测
            if (AppConfig.IgnoreAuthRight)
            {
                return;
            }


            //若该用户存在该页面权限，则直接return
            Tright_User_Role_Da userrole = new Tright_User_Role_Da();
            if (userrole.ListByVm(userid, page).Count() > 0)
            {
                return;
            }


            //是否ajax请求，是ajax 则判定为 请求操作， 非ajax则判定为 访问页面
            if (isAjax)
            {

                Context.Result = new JsonResult(new { Success = false, Code = 405, Message = "您没有该功能操作权限！" });
                return;

            }

            //跳转指定的没有权限的页面
            Context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "UserRight",
                action = "NoPermission"
            })); 
            
            return;

        }


        /// <summary>
        /// 给用户设置默认登录角色
        /// </summary>
        /// <returns></returns>

        public void SetDefaultRole(int userid) {

            Tright_User_Role_Da userrole = new Tright_User_Role_Da();

            if (userrole.Where(s => s.Userid == userid).Count() <= 0)
            {
                Tright_User_Role userolemodel = new Tright_User_Role()
                {
                    Roleid = 1,   //默认1为普通会员
                    Userid = userid
                };

                userrole.Insert(userolemodel);
            }

        }

        /// <summary>
        /// 获取当前页面 或 功能 的路由地址
        /// </summary>
        /// <param name="Context"></param>
        /// <returns></returns>
        public string GetPageUrl(ActionExecutingContext Context, ref string areaName,ref string controllerName, ref string actionName) {


            if (Context.ActionDescriptor.RouteValues.ContainsKey("area"))
            {
                areaName = Context.ActionDescriptor.RouteValues["area"].ToString();
            }
            if (Context.ActionDescriptor.RouteValues.ContainsKey("controller"))
            {
                controllerName = Context.ActionDescriptor.RouteValues["controller"].ToString();
            }
            if (Context.ActionDescriptor.RouteValues.ContainsKey("action"))
            {
                actionName = Context.ActionDescriptor.RouteValues["action"].ToString();
            }



            var page = "/" + controllerName + "/" + actionName;

            if (!string.IsNullOrEmpty(areaName))
            {
                page = "/" + areaName + page;
            }

            return page;

        }


        /// <summary>
        /// 根据Action自动添加功能
        /// </summary>
        /// <returns></returns>
        public void AddActionFunc(string controllerName,string actionName,string areaName,string page,bool isAjax)
        {


            //数据库是否存在该页面配置
            Tright_Power_Da pwmanager = new Tright_Power_Da();
            bool HasPage = pwmanager.Where(s => s.Pageurl.ToLower() == page.ToLower()).Count() <= 0;


            if (HasPage)
            {

                Tright_Power powermodel = new Tright_Power
                {
                    Controller = controllerName,
                    Action = actionName,
                    Area = areaName,
                    Powername = PowerName,
                    Pageurl = page.ToLower()
                };

                if (isAjax)
                {
                    // 添加一个功能功能操作的权限
                    var m = pwmanager.Where(s => s.Controller == controllerName && s.Powertype == (int)PowerType.页面访问).First();

                    powermodel.Parentid = m.Id;
                    powermodel.Powertype = (int)PowerType.功能操作;

                }
                else
                {
                    //添加一个 页面访问 权限
                    powermodel.Parentid = 0;
                    powermodel.Powertype = (int)PowerType.页面访问;

                }

                pwmanager.Insert(powermodel);

            }



        }

    }
}
