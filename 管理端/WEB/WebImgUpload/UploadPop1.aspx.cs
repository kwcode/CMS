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
        protected void ibUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (file.FileBytes.Length > 0)
                {
                    FileInfo fileInfo = new FileInfo(file.PostedFile.FileName);
                    string extension = System.IO.Path.GetExtension(fileInfo.Name);
                    string fileName = UICommon.Util.GetRandNumber(15) + extension;
                    string FilePath = UICommon.FileHelper.GetFilePath(userInfo.ID, UICommon.FileNameIndex.Picture, fileName);
                    string filename = Server.MapPath(FilePath);
                    file.SaveAs(filename);
                    GocParentConfirm(FilePath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GocParentConfirm(string images)
        {
            string script = "<script>parent.window.tw.confirm('" + images + "'); </script>";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", script);

        }
    }
}