using Core.Common.CoreFrame;
using Core.Common.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Linq;

namespace Lobster.Service.Demo.Dao
{
    /// <summary>
    /// Demo DAO
    /// </summary>
    public class BookDao : AbstractDao
    {
        /// <summary>
        /// 获取书籍数据
        /// </summary>
        /// <param name="bookName">查询名称</param>
        /// <param name="pageinfo">分页对象</param>
        /// <returns></returns>
        public dynamic GetBookData(string bookName, ref PageInfo pageinfo)
        {
            string strSql = @"select * from books ";
            strSql = SqlPage.FormatSql(DatabaseType.SqlServer, strSql, pageinfo, connection);
            return connection.Query<Entity.Book>(strSql);
        }

    }
}
