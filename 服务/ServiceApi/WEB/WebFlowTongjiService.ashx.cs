using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEB
{
    /// <summary>
    ///  网站统计服务
    /// </summary>
    public class WebFlowTongjiService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                Uri urlRef = context.Request.UrlReferrer;
                if (urlRef != null)
                {
                    string host = urlRef.Host;
                    string BrowseUrl = urlRef.OriginalString;
                    int UserID = DAL.DomainDAL.GetUserID(host);

                    string SourceUrl = context.Request["SourceUrl"];
                    string IP = HTTPS.GetIP;
                    SqlParameter[] pramsAdd =
                    {
                         DALUtil.MakeInParam("@UserID",SqlDbType.Int,4,UserID),
                         DALUtil.MakeInParam("@IP",SqlDbType.NVarChar,200,IP),
                         DALUtil.MakeInParam("@IPSourceUrl",SqlDbType.NVarChar,1000,SourceUrl),
                         DALUtil.MakeInParam("@BrowseUrl",SqlDbType.NVarChar,1000,BrowseUrl),
                    };
                    DAL.WebFlowTongJiDAL.Add(pramsAdd); 
                }
            }
            catch { }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}