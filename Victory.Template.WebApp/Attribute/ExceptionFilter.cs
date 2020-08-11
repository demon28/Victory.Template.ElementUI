using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Victory.Core.Extensions;
using Victory.Core.Helpers;
using Victory.Template.DataAccess.CodeGenerator;
using Victory.Template.Entity.CodeGenerator;
using Victory.Template.Entity.Enums;

namespace Victory.Template.WebApi.Attribute
{
    public class ExceptionFilter:IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;

            Log4netHelper.Error(ex.Message);//记录错误日志
            InsertSysLog(ex.Message);   //添加到数据库

            context.ExceptionHandled = true; //代表异常已经处理，不会再跳转到开发调试时的异常信息页，可以跳转到我们下面自定义的方法中。若开发过程可以将 该行注释掉，则直接抛出异常调试

            //通过HTTP请求头来判断是否为Ajax请求，Ajax请求的request headers里都会有一个key为x-requested-with，值“XMLHttpRequest”
            var requestData = context.HttpContext.Request.Headers.ContainsKey("x-requested-with");
            bool IsAjax = false;
            if (requestData)
            {
                IsAjax = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest" ? true : false;
            }
            if (!IsAjax)//不是异步请求则跳转页面，异步请求则返回json
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Error",
                    message= WebUtility.HtmlEncode(ex.Message)
                }));

            }
            else
            {
                context.Result = new JsonResult(new { Success = false, Code = 500, Message = ex.Message});
            }
        }



       /// <summary>
       /// 将异常日志写入数据库
       /// </summary>
       /// <param name="message"></param>
        private void InsertSysLog(string message)
        {
            Tsys_Log_Da da = new Tsys_Log_Da();

            Tsys_Log model = new Tsys_Log() {
                Content = message,
                Createtime = DateTime.Now,
                Type= (int)SysLogType.系统异常,
            };
            da.Insert(model);
        }
    }
}
