using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Victory.Template.Entity.CodeGenerator
{
    /// <summary>
    ///  系统日志表
    ///</summary>
    public class   Tsys_Log
    {

       public Tsys_Log()
       {
      
       }

        ///<summary>
        ///描述：
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：1，用户登录。2，系统异常。3操作日志
        ///</summary>
        public int Type { get; set; }
        ///<summary>
        ///描述：
        ///</summary>
        public string Content { get; set; }
        ///<summary>
        ///描述：
        ///</summary>
        public DateTime Createtime { get; set; }
        ///<summary>
        ///描述：
        ///</summary>
        public string Reamarks { get; set; }

    }
 }








