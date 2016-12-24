using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace UICommon
{
    public class FileHelper
    {
        /// <summary>
        /// 获取相对路径   /upload/10/ContentPictures/2010/10/2/aaa.jpg
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="FileNameIndex"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePath(int UserID, FileNameIndex FileNameIndex, string fileName)
        {
            string dir = GetDirectory(UserID, FileNameIndex);
            string serverPath = dir + "/" + fileName;
            return serverPath;
        }
        /// <summary>
        /// 获取完整的路径 C://upload/10/ContentPictures/2010/10/2/aaa.jpg
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="FileNameIndex"></param>
        /// <param name="fileName">文件名带后缀</param>
        /// <returns></returns>
        public static string GetFullFilePath(int UserID, FileNameIndex FileNameIndex, string fileName)
        {
            string dir = GetDirectory(UserID, FileNameIndex);
            string serverPath = HttpContext.Current.Server.MapPath(dir) + "/" + fileName;
            return serverPath;
        }
        /// <summary>
        /// 根据用户ID生成图片路径 如:/upload/10/ContentPictures/2010/10/2
        /// </summary>
        /// <param name="UserID">用户ID 不重复的</param>
        /// <param name="FileNameIndex">文件夹目录</param>

        /// <returns></returns>
        public static string GetDirectory(int UserID, FileNameIndex FileNameIndex)
        {
            string dir = string.Empty;
            try
            {

                string file = ((FileNameIndex)FileNameIndex).ToString();
                string pathFile = "/upload/" + UserID + "/" + file + "/" + DateTime.Now.ToString("yyyy/MM/dd");
                string serverPath = HttpContext.Current.Server.MapPath(pathFile);
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                dir = pathFile;
            }
            catch (Exception)
            {

                throw;
            }
            return dir;
        }


        /// <summary>
        /// 生成缩小的图片
        /// </summary>
        /// <param name="oldfile">旧图片 相对路径</param>
        /// <param name="saveImg">保存的新图片 只是图片名称</param>
        /// <param name="Width">宽度</param>
        /// <param name="Height">高度</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, System.Double Width, System.Double Height)
        {
            //现在要保存的绝对路径
            string strGoodFile = "";
            //文件信息
            FileInfo fileInfo = new FileInfo(originalImagePath);
            strGoodFile = fileInfo.Directory + "\\" + thumbnailPath;
            System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
            if (image.Width <= Width && image.Height <= Height)//小于300不用缩图
            {
                File.Copy(originalImagePath, strGoodFile, true);
                image.Dispose();
            }
            else
            {
                System.Double NewWidth, NewHeight;
                if (image.Width > image.Height)
                {
                    NewWidth = Width;
                    NewHeight = image.Height * (NewWidth / image.Width);
                }
                else
                {
                    NewHeight = Height;
                    NewWidth = (NewHeight / image.Height) * image.Width;
                }

                //取得图片大小
                if (NewWidth < 1)
                {
                    NewWidth = 1;
                }

                System.Drawing.Size size = new Size((int)NewWidth, (int)NewHeight);

                //新建一个bmp图片
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(size.Width, size.Height);

                //新建一个画板
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空一下画布
                //g.Clear(Color.White);
                g.Clear(Color.Transparent);//透明背景方式

                //在指定位置画图
                g.DrawImage(image, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                    System.Drawing.GraphicsUnit.Pixel);


                //保存高清晰度的缩略图
                bitmap.Save(strGoodFile, System.Drawing.Imaging.ImageFormat.Jpeg);

                g.Dispose();
                image.Dispose();
                bitmap.Dispose();
            }

        }

        public static string GetFileTxt(string path)
        {
            System.IO.FileStream fsr = System.IO.File.OpenRead(path);
            System.IO.StreamReader sr = new System.IO.StreamReader(fsr); 
            string record = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            fsr.Close();
            fsr.Dispose();
            return record;
        }
    }
    /// <summary>
    /// 根据传递不多的使用类型创建枚举
    /// </summary>
    public enum FileNameIndex
    {
        Authentication = 0,  //身份认证
        OperatingLicense = 1,  //运营许可证
        DrivingLicense = 2,  //驾驶证
        TravelLicense = 3, //行驶证
        GuideCertification = 4,  //导游认证
        Cooperation = 5,   //代理合作
        UploadWorks = 6, //上传作品
        UploadImg = 7, //上传图片
        Picture = 8, //用户头像
        Advertising = 9, //广告横幅的枚举
        Blogroll = 10, //友情链接的枚举
        OutlinkImg = 11, //外链图片的自动保存
        ThemesImg = 12, //主题图片
        LeaderPhoto = 13, //主题图片
        ActivityPicture = 14,
        TempPicture = 99,//临时图片 
        QRcode = 15,  //二维码
        HeadThemeBg = 16,         //头部背景图 
        MenPiaoImg = 17,  //门票图片
        ContentPictures = 18,//内容图片
        ThemePictures = 19, //主题图片
        TitlePictures = 20,//标题图片
    }
}
