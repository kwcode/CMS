using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WEB.WebImgUpload
{
    public partial class WA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int wmkType = UICommon.Util.ConvertToInt32(Request["wmkType"] ?? "0");//类型
            int wmkPosition = UICommon.Util.ConvertToInt32(Request["wmkPosition"] ?? "4");
            string YanTuPath = UICommon.WatermarkUtil.GetMapPathYanTuPath;//原图 
            System.Drawing.Image image = System.Drawing.Image.FromFile(YanTuPath);
            //加水印 
            Graphics graph = Graphics.FromImage(image);
            string _familyName = Request["familyName"] ?? "arial";
            FontStyle _fontStyle = (FontStyle)UICommon.Util.ConvertToInt32(Request["fontStyle"] ?? "1");//?? FontStyle.Bold;
            float _size = UICommon.Util.ConvertToInt32(Request["size"] ?? "16");// 16;
            string _watermarkText = Request["watermarkText"];
            string rColor = Server.UrlDecode(Request["color"]);
            float _alpha = Convert.ToSingle(Convert.ToInt32(Request["alpha"] ?? "50") / 100F);// 16;
            Color color = ToColor(rColor, _alpha);
            double _watermarkSizePercent = Convert.ToDouble(Request["watermarkSizePercent"]);
            if (wmkType == 1)
            {
                UICommon.WatermarkUtil.addWatermarkImage(graph, UICommon.WatermarkUtil.GetMapPathWaterPath, wmkPosition, image.Width, image.Height, _alpha, _watermarkSizePercent);
            }
            else
            {
                UICommon.WatermarkUtil.addWatermarkText(graph, _watermarkText, wmkPosition, image.Width, image.Height, color, _familyName, _fontStyle, _size);
            }
            //System.Drawing.Image.

            byte[] buff = ConverImageToByte(image);
            Response.ClearContent();
            Response.ContentType = "image/gif";
            Response.BinaryWrite(buff);

        }
        public byte[] ConverImageToByte(System.Drawing.Image image)
        {

            Bitmap bmp = new Bitmap(image);
            image.Dispose();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();

        }

        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color">带#号的16进制颜色</param>
        /// <returns></returns>
        public Color ToColor(string color, float _alpha)
        {
            ////Color.FromArgb(argb // Color.Red;//  Color.FromArgb(153, 0, 0, 0);
            try
            {

                int red, green, blue = 0;
                char[] rgb;
                color = color.TrimStart('#');
                color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
                switch (color.Length)
                {
                    case 3:
                        rgb = color.ToCharArray();
                        red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                        green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                        blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                        return Color.FromArgb(red, green, blue);
                    case 6:
                        rgb = color.ToCharArray();
                        red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                        green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                        blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                        //Color.FromArgb(0-255 Alpha 这个决定透明度, 255, 0, 0));
                        int alpha = Convert.ToInt32(255 * _alpha);
                        return Color.FromArgb(alpha, red, green, blue);
                    default:
                        return Color.FromName(color);

                }
            }
            catch (Exception ex)
            {
                return Color.FromArgb(153, 0, 0, 0);
            }
        }

    }
}