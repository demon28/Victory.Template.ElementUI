using FreeSql.DataAnnotations;
using System;
using System.Linq;
using System.Text;



namespace Victory.Template.Entity.CodeGenerator
{
    /// <summary>
    ///  角色与权限关联中间表
    ///</summary>
    public class   Tright_Role_Power
    {

       public Tright_Role_Power()
       {
      
       }
        [Column(IsIdentity = true, IsPrimary = true)]
        ///<summary>
        ///描述：
        ///</summary>
        public int Id { get; set; }
        
        ///<summary>
        ///描述：角色id
        ///</summary>
        public int Roleid { get; set; }
        
        ///<summary>
        ///描述：权限id
        ///</summary>
        public int Powerid { get; set; }
        

    }
 }









