using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.WebImgUpload
{
    public partial class UploadPop1 : UICommon.BasePage_PM
    {

        /// <summary>
        /// 用户图片规格
        /// </summary>
        public int UserPictureSpec_ValueNum
        {
            get
            {
                return UICommon.Util.ConvertToInt32(Request["UserPictureSpec_ValueNum"]);
            }
        }
        protected void ibUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (file.FileBytes.Length > 0)
                {
                    UserPictureEntity entity = UICommon.Picture.ImageUpload.upload(file.PostedFile, UserPictureSpec_ValueNum, userInfo.ID);
                    if (entity != null && entity.ID > 0)
                    {
                        PictureModel pModel = new PictureModel();
                        pModel.id = entity.ID;
                        pModel.images = entity.Tn;
                        string jsonPicture = Newtonsoft.Json.JsonConvert.SerializeObject(pModel);
                        GocParentConfirm(jsonPicture);
                    }
                    else
                    {
                        UICommon.ScriptHelper.Alert("上传失败");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GocParentConfirm(string images)
        {
            string script = "<script>parent.window.tw.confirm('" + images + "'); </script>";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", script);

        }


        private class PictureModel
        {
            public int id { get; set; }
            public string images { get; set; }
        }
    }
}