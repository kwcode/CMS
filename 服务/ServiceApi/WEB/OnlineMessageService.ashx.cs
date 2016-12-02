using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WEB
{
    /// <summary>
    /// OnlineMessageService 的摘要说明
    /// </summary>
    public class OnlineMessageService : UICommon.BaseHandler
    {
        UICommon.ResponseData responseData = new UICommon.ResponseData() { code = UICommon.ReturnCode.error, msg = "无效请求。", data = null };
        public override UICommon.ResponseData ProcessResponse(HttpContext context)
        {
            try
            {
                string action = Request["action"];
                switch (action)
                {
                    //在线留言
                    case "OnlineMessage":
                        OnlineMessage();
                        break;
                    default:
                        {
                            responseData.msg = "无效方法。";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                responseData.msg = ex.Message;
                responseData.data = ex.ToString();
            }

            return responseData;
        }

        #region 在线留言

        private void OnlineMessage()
        {
            try
            {
                Model.ServiceApiConfigurationEntity entityServiceApiConfiguration = DAL.ServiceApiConfigurationDAL.Get_98(1, "ServiceUrl,Token");

                string token1 = Request["token"];
                if (entityServiceApiConfiguration.Token != token1)
                {
                    responseData.msg = "token无效";
                    return;
                }
                int UserID = UICommon.Util.ConvertToInt32(Request["UserID"]);
                if (UserID <= 0)
                {
                    responseData.msg = "提交留言失败,无效用户！";
                    return;
                }
                string IP = UICommon.Util.ConvertToString(Request["IP"]);
                //检测IP 一个ip 留言间隔10秒
                bool isIPOk = true;
                try
                {
                    OnlineMessageEntity entity = DAL.OnlineMessageDAL.Get1<Model.OnlineMessageEntity>(" TOP 1 CreateTS,IP,UserID", null, "CreateTS DESC");
                    if (IP == entity.IP && UserID == entity.UserID)
                    {
                        DateTime nowTime = DateTime.Now;
                        DateTime LastTime = entity.CreateTS;
                        if (LastTime.AddSeconds(10) > nowTime)
                        {
                            isIPOk = false;
                        }
                    }


                }
                catch { }

                if (!isIPOk)
                {
                    responseData.msg = "请勿频繁提交留言失败，请稍后再试！";
                    return;
                }

                string Title = UICommon.Util.ConvertToString(Request["Title"]);
                string TxtContent = UICommon.Util.ConvertToString(Request["TxtContent"]);
                string RealName = UICommon.Util.ConvertToString(Request["RealName"]);
                string Email = UICommon.Util.ConvertToString(Request["Email"]);
                string Phone = UICommon.Util.ConvertToString(Request["Phone"]);
                string Address = UICommon.Util.ConvertToString(Request["Address"]);
                string QQ = UICommon.Util.ConvertToString(Request["QQ"]);
                string Contacts = UICommon.Util.ConvertToString(Request["Contacts"]);


                SqlParameter[] pramsAdd =
			    {
				    DALUtil.MakeInParam("@UserID",SqlDbType.Int,4,UserID),
                    DALUtil.MakeInParam("@Title",SqlDbType.NVarChar,200,Title),
                    DALUtil.MakeInParam("@TxtContent",SqlDbType.NText,TxtContent.Length,TxtContent),
                    DALUtil.MakeInParam("@RealName",SqlDbType.NVarChar,200,RealName),

                    DALUtil.MakeInParam("@Email",SqlDbType.NVarChar,200,Email),
                    DALUtil.MakeInParam("@Phone",SqlDbType.NVarChar,200,Phone),
                    DALUtil.MakeInParam("@Address",SqlDbType.NVarChar,200,Address),
                    DALUtil.MakeInParam("@QQ",SqlDbType.NVarChar,200,QQ),
                    DALUtil.MakeInParam("@Contacts",SqlDbType.NVarChar,200,Contacts),
                    DALUtil.MakeInParam("@IP",SqlDbType.NVarChar,200,IP),
			    };
                int row_Add = DAL.OnlineMessageDAL.Add(pramsAdd);
                if (row_Add > 0)
                {
                    responseData.code = UICommon.ReturnCode.success;
                    responseData.msg = "提交成功！";
                }
                else
                {
                    responseData.msg = "提交留言失败！";
                }
            }
            catch (Exception ex)
            {
                responseData.msg = ex.Message;
            }
        }

        #endregion
    }
}