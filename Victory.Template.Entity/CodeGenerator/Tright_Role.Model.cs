using FreeSql.DataAnnotations;
using System;
using System.Linq;
using System.Text;



namespace Victory.Template.Entity.CodeGenerator
{
    /// <summary>
    ///  角色表
    ///</summary>
    public class   Tright_Role
    {

       public Tright_Role()
       {
      
       }
        [Column(IsIdentity = true, IsPrimary = true)]
        ///<summary>
        ///描述：
        ///</summary>
        public int Id { get; set; }
        
        ///<summary>
        ///描述：角色名称
        ///</summary>
        public string Rolename { get; set; }
        
        ///<summary>
        ///描述：
        ///</summary>
        public string Remarks { get; set; }
        

    }
 }









