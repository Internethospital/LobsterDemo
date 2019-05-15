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
        /// 根据机构ID获取应用信息
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="pageinfo">页码</param>
        /// <returns>应用信息</returns>
        public dynamic GetAppData(int workId, bool isforbidden, string appName, ref PageInfo pageinfo)
        {
            
            string strWhere = "";
            string strSql = @" select AppID, AppKey, AppName, AppImage, AppUrl, CreateUser, DelFlag, WorkId, AppType, SortNo,
                                    case DelFlag when 0 then '已启用' else '已停用' end as StrDelFlag,
                                    case AppType when 1 then '用户集成' else '权限集成' end as StrAppType,
                                    CONVERT(varchar(19), CreateDate, 20) as CreateDate                    
                                    from SSO_App ";
            if (isforbidden)
            {
                strWhere += " and DelFlag=0 ";
            }

            if (workId > 0)
            {
                strWhere += string.Format(" and WorkId={0} ", workId);
            }

            if (!string.IsNullOrEmpty(appName))
            {
                strWhere += string.Format(" and AppName like'%{0}%' ", appName);
            }

            if (strWhere != "")
            {
                strSql = strSql + " where " + strWhere.Trim().Substring(3);
            }

            pageinfo.OrderBy = new string[] { "SortNo" };

            strSql = SqlPage.FormatSql(DatabaseType.SqlServer, strSql, pageinfo, connection);
            return connection.Query<SSO_App>(strSql);
        }

    }
}
