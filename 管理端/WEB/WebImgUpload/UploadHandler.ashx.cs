using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UICommon;

namespace WEB.WebImgUpload
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : BaseHandler
    {
        ResponseData responseData = new ResponseData()
        {
            code = ReturnCode.error,
            msg = "未配置",
            data = null
        };
        protected override ResponseData ProcessResponse(HttpContext context)
        {
            int UserID = UICommon.SessionAccess.UserID;
            if (UserID <= 0)
            {
                responseData.msg = "登录异常,请刷新重试！";
                return responseData;
            }
            Response.CacheControl = "no-cache";
            if (Request.Files.Count > 0)
            {
                try
                {
                    for (int j = 0; j < Request.Files.Count; j++)
                    {
                        int offset = Convert.ToInt32(Request["chunk"]); //当前分块
                        int total = Convert.ToInt32(Request["chunks"]);//总的分块数量
                        string name = Request["name"];
                        string fileid = Request["fileid"];
                        int UserPictureSpec_ValueNum = UICommon.Util.ConvertToInt32(Request["UserPictureSpec_ValueNum"]);
                        int BookID = UICommon.Util.ConvertToInt32(Request["BookID"]);
                        int IsWatermark = UICommon.Util.ConvertToInt32(Request["IsWatermark"]);
                        HttpPostedFile uploadFile = Request.Files[j];

                        UserPictureEntity entity = UICommon.Picture.ImageUpload.upload(uploadFile, UserID, BookID, IsWatermark);
                        #region 上传
                        // UserPictureEntity entity = UICommon.Picture.ImageUpload.upload(file.PostedFile, UserPictureSpec_ValueNum, userInfo.ID);
                        if (entity != null && entity.ID > 0)
                        {
                            responseData.msg = "上传成功";
                            responseData.data = entity;

                        }
                        else
                        {
                            responseData.msg = "上传失败";
                        }
                        #endregion

                    }
                }
                catch (Exception ex)
                {
                    responseData.msg = ex.Message;
                    responseData.data = ex.ToString();
                }

            }
            return responseData;
        }
    }
}