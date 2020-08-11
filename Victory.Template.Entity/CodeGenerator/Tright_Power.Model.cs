using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Victory.Template.Entity.CodeGenerator
{
    /// <summary>
    ///  权限表
    ///</summary>
    public class   Tright_Power
    {

       public Tright_Power()
       {
      
       }
        [Column(IsIdentity = true, IsPrimary = true)]
        ///<summary>
        ///描述：
        ///</summary>
        public int Id { get; set; }
        
        ///<summary>
        ///描述：描述：权限类型，0,菜单，1页面，2功能
        ///</summary>
        public int Powertype { get; set; }
        

        [Required]
        ///<summary>
        ///描述：权限名称
        ///</summary>
        public string Powername { get; set; }
        
        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remarks { get; set; }


        ///<summary>
        ///描述：父级ID
        ///</summary>
        public int Parentid { get; set; }
        
        ///<summary>
        ///描述：PAGEURL
        ///</summary>
        public string Pageurl { get; set; }
        
        ///<summary>
        ///描述：控制器
        ///</summary>
        public string Controller { get; set; }
        
        ///<summary>
        ///描述：行为
        ///</summary>
        public string Action { get; set; }
        
        ///<summary>
        ///描述：排序id
        ///</summary>
        public int Sortid { get; set; }
        
        ///<summary>
        ///描述：域
        ///</summary>
        public string Area { get; set; }

        [JsonIgnore]
        public Tright_Power Parent { get; set; }

        public List<Tright_Power> Childs { get; set; }

    }
 }









