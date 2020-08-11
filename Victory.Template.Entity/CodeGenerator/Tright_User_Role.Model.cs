using FreeSql.DataAnnotations;
using System;
using System.Linq;
using System.Text;



namespace Victory.Template.Entity.CodeGenerator
{
    /// <summary>
    ///  用户角色中间关联表
    ///</summary>
    public class   Tright_User_Role
    {

       public Tright_User_Role()
       {
      
       }
        [Column(IsIdentity = true, IsPrimary = true)]
        ///<summary>
        ///描述：
        ///</summary>
        public int Id { get; set; }
        
        ///<summary>
        ///描述：
        ///</summary>
        public int Userid { get; set; }
        
        ///<summary>
        ///描述：
        ///</summary>
        public int Roleid { get; set; }
        

    }
 }









