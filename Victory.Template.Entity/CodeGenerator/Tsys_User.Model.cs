using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Victory.Template.Entity.CodeGenerator
{
    /// <summary>
    ///  
    ///</summary>
    public class   Tsys_User
    {

       public Tsys_User()
       {
      
       }

        ///<summary>
        ///描述：主键
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：姓名
        ///</summary>
        public string Name { get; set; }
        ///<summary>
        ///描述：密码
        ///</summary>
        public string Pwd { get; set; }
        ///<summary>
        ///描述：工号
        ///</summary>
        public string Workid { get; set; }
        ///<summary>
        ///描述：手机号
        ///</summary>
        public string Phone { get; set; }
        ///<summary>
        ///描述：状态默认为0
        ///</summary>
        public int Status { get; set; }
        ///<summary>
        ///描述：性别0：男 1，女
        ///</summary>
        public int Sex { get; set; }
        ///<summary>
        ///描述：一级部门
        ///</summary>
        public string Dep1 { get; set; }
        ///<summary>
        ///描述：二级部门
        ///</summary>
        public string Dep2 { get; set; }
        ///<summary>
        ///描述：三级部门
        ///</summary>
        public string Dep3 { get; set; }
        ///<summary>
        ///描述：四级部门
        ///</summary>
        public string Dep4 { get; set; }
        ///<summary>
        ///描述：五级部门
        ///</summary>
        public string Dep5 { get; set; }
        ///<summary>
        ///描述：职位
        ///</summary>
        public string Dtname { get; set; }
        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime Createtime { get; set; }
        ///<summary>
        ///描述：
        ///</summary>
        public string Remarks { get; set; }

        ///<summary>
        ///描述：入职时间
        ///</summary>
        public DateTime Comedate { get; set; }

    }
}








