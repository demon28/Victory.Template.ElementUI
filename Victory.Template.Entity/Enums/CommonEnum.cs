using System;
using System.Collections.Generic;
using System.Text;

namespace Victory.Template.Entity.Enums
{
    /// <summary>
    /// 系统日志类型
    /// </summary>
    public enum SysLogType
    {
        全部=0,
        登录日志=1,
        系统异常=2,
        操作日志=3
    }



    // <summary>
    /// 权限类型
    /// </summary>
    public enum PowerType
    {
        系统菜单 = 0,
        页面访问 = 1,
        功能操作 = 2
    }
}
