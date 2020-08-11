
using System;
using System.Collections.Generic;
using System.Linq;
using Victory.Template.Entity.CodeGenerator;

namespace Victory.Template.DataAccess.CodeGenerator
{

    /// <summary>
    ///  角色与权限关联中间表
    ///</summary>
    public class Tright_Role_Power_Da : FreeSql.BaseRepository<Tright_Role_Power>
    {
        public Tright_Role_Power_Da() : base(DataAccess.DbContext.Db, null, null)
        {


        }

    }

}

