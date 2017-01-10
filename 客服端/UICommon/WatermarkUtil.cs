using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace UICommon
{
    public class WatermarkUtil
    {
        /// <summary>
        /// 获取测试原图
        /// </summary>
        public static string GetMapPathYanTuPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("/images/watermarkPreview.png");
            }
        }
        /// <summary>
        /// 获取加水印图片
        /// </summary>
        public static string GetMapPathWaterPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("/upload/w/w.jpg");
            }
        }

        /// <summary>
        ///  加水印文字
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="_watermarkText">水印文字内容</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        public static void addWatermarkText(Graphics picture, string _watermarkText,
            int _watermarkPosition, int _width, int _height, Color color
            , string _familyName = "arial", FontStyle _fontStyle = FontStyle.Bold, float _size = 16)
        {
            Font crFont = new Font("华文彩云", _size, _fontStyle);
            SizeF crSize = picture.MeasureString(_watermarkText, crFont);

            float xpos = 0;
            float ypos = 0;

            #region 水印的位置

            switch (_watermarkPosition)//水印的位置
            {
                /*
                 * 0：左上角
                 * 1：正上方
                 * 2：右上角
                 * 3：左中处
                 * 4：正中处
                 * 5：右中处
                 * 6：左下角
                 * 7：正下方
                 * 8：右下角 
                 */
                case 0://左上
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);//(float).01)=0.01
                    ypos = (float)_height * (float).01;
                    break;
                case 1://正上方
                    xpos = ((float)_width * (float)0.5);// + (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case 2://右上
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case 3://左中处
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = (float)_height * (float)0.5;
                    break;
                case 4://正中处
                    xpos = ((float)_width * (float).5);
                    ypos = (float)_height * (float).5;
                    break;
                case 5://右中处
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = ((float)_height * (float).5) - crSize.Height;
                    break;
                case 6://左下
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
                case 7://正下方
                    xpos = ((float)_width * (float).5);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
                case 8://右下
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;

            }
            #endregion

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            SolidBrush semiTransBrush = new SolidBrush(color);//Color.FromArgb(153, 255, 255, 255)
            picture.DrawString(_watermarkText, crFont, semiTransBrush, xpos, ypos, StrFormat);

            semiTransBrush.Dispose();

        }



        /// <summary>
        ///  加水印图片
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="WaterMarkPicPath">水印图片的地址</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        /// <param name="_alpha">透明度0.1-1</param>
        /// <param name="watermarkSizePercent">水印缩放比例 按被加水印图片 默认1/4</param>
        public static void addWatermarkImage(Graphics picture, string WaterMarkPicPath, int _watermarkPosition, int _width, int _height, float _alpha, double watermarkSizePercent)
        {
            //加载水印图片
            Image watermark = new Bitmap(WaterMarkPicPath);
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {
                                                 new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  _alpha, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                             };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 0.25;
            //计算水印图片的比率 watermarkSizePercent 
            #region 计算水印图片的比率
            if (watermarkSizePercent > 0.1 && watermarkSizePercent < 1)
            {
                bl = Convert.ToDouble(_width * watermarkSizePercent) / Convert.ToDouble(watermark.Width);
            }
            WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height * bl);

            #endregion
            int xpos = 0;
            int ypos = 0;
            switch (_watermarkPosition)
            {
                /*
            * 0：左上角
            * 1：正上方
            * 2：右上角
            * 3：左中处
            * 4：正中处
            * 5：右中处
            * 6：左下角
            * 7：正下方
            * 8：右下角 
            */
                case 0://左上角
                    xpos = 10;
                    ypos = 10;
                    break;
                case 1://正上方
                    xpos = (_width / 2) - (WatermarkWidth / 2);
                    ypos = 10;
                    break;
                case 2://右上角
                    xpos = _width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case 3://左中处
                    xpos = 10;
                    ypos = (_height / 2) - (WatermarkHeight / 2);
                    break;
                case 4://正中处
                    xpos = (_width / 2) - (WatermarkWidth / 2);
                    ypos = (_height / 2) - (WatermarkHeight / 2);
                    break;
                case 5://右中处
                    xpos = _width - WatermarkWidth - 10;
                    ypos = (_height / 2) - (WatermarkHeight / 2);
                    break;
                case 6://左下角
                    xpos = 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
                case 7://正下方
                    xpos = (_width / 2) - (WatermarkWidth / 2);
                    ypos = _height - WatermarkHeight - 10;
                    break;
                case 8:   //右下角 
                    xpos = _width - WatermarkWidth - 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;

            }

            picture.DrawImage(watermark, new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);


            watermark.Dispose();
            imageAttributes.Dispose();
        }


        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color">带#号的16进制颜色</param>
        /// <returns></returns>
        public static Color ToColor(string color, float _alpha)
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