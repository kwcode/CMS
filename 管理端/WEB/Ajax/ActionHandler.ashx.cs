using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UICommon;

namespace WEB.Ajax
{
    /// <summary>
    /// UserPictureBooHandler 的摘要说明
    /// </summary>
    public class ActionHandler : BaseHandler
    {
        ResponseData responseData = new ResponseData()
        {
            code = ReturnCode.error,
            msg = "未配置",
            data = null
        };
        int UserID = 0;
        protected override ResponseData ProcessResponse(HttpContext context)
        {
            UserID = UICommon.SessionAccess.UserID;
            if (UserID <= 0)
            {
                responseData.msg = "登录异常,请刷新重试！";
                return responseData;
            }
            try
            {
                string action = Request["action"];
                switch (action)
                {
                    //编辑相册
                    case "SaveUserPictureBook":
                        SaveUserPictureBook();
                        break;
                    //设置图片标题
                    case "SetUserPictureTitle":
                        SetUserPictureTitle();
                        break;
                    //删除图片
                    case "DelUserPicture":
                        DelUserPicture();
                        break;

                    default:
                        {
                            responseData.msg = "无效方法";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                responseData.msg = ex.Message;
                responseData.data = ex.ToString();
            }
            return responseData;
        }

        private void DelUserPicture()
        {
            int id = Util.ConvertToInt32(Request["id"]);
            SqlParameter[] pramsModify =
                {
                    DAL.DALUtil.MakeInParam("@IsDel",System.Data.SqlDbType.Int,4,1),  
                    DAL.DALUtil.MakeInParam("@DelTime",System.Data.SqlDbType.DateTime,8,DateTime.Now),
                };
            int row_Mod = DAL.UserPictureDAL.Modify(pramsModify, id, UserID);
            if (row_Mod > 0)
            {
                //临时删除并把对应的图片移动到临时的文件夹里面 Temp
                UICommon.Picture.ImageUpload.MoveFile(id, UserID);
                responseData.code = ReturnCode.success;
                responseData.msg = "保存成功";
            }
            else
            {
                responseData.msg = "保存失败";
            }
        }
        #region 设置图片标题

        private void SetUserPictureTitle()
        {
            int id = Util.ConvertToInt32(Request["id"]);
            string name = Request["name"].Trim();
            SqlParameter[] pramsModify =
                {
                    DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100,name),  
                };
            int row_Mod = DAL.UserPictureDAL.Modify(pramsModify, id, UserID);
            if (row_Mod > 0)
            {
                responseData.code = ReturnCode.success;
                responseData.msg = "保存成功";
            }
            else
            {
                responseData.msg = "保存失败";
            }
        }
        #endregion

        #region 编辑相册

        private void SaveUserPictureBook()
        {
            int id = Util.ConvertToInt32(Request["id"]);
            string name = Request["name"].Trim();
            if (string.IsNullOrEmpty(name))
            {
                responseData.msg = "请输入名称";
                return;
            }
            int ordernum = Util.ConvertToInt32(Request["ordernum"]);
            if (ordernum < 1)
            {
                responseData.msg = "请输入正确的排序值";
                return;
            }
            int result = 0;
            if (id > 0)
            {
                SqlParameter[] pramsModify =
                {
                    DAL.DALUtil.MakeInParam("@Name",System.Data.SqlDbType.NVarChar,100,name), 
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,ordernum),
                };
                result = DAL.UserPictureBookDAL.Modify(pramsModify, id);
            }
            else
            {
                SqlParameter[] pramsAdd =
                {
                    DAL.DALUtil.MakeInParam("@Name",System.Data.SqlDbType.NVarChar,100,name),
                    DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,UserID),
                    DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,ordernum),
                };
                result = DAL.UserPictureBookDAL.Add(pramsAdd);
            }
            if (result > 0)
            {
                responseData.code = ReturnCode.success;
                responseData.msg = "保存成功";
            }
            else
            {
                responseData.msg = "保存失败";
            }
        }
        #endregion
    }
}