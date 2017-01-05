using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace UICommon.Picture
{
    public class ImageUpload
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image_upload">图片文件</param>
        /// <param name="UserID">用户ID</param>
        /// <param name="BookID">相册ID</param>
        /// <param name="IsWatermark">是否增加水印</param>
        /// <returns></returns>
        public static UserPictureEntity upload(HttpPostedFile image_upload, int UserID, int BookID = 0, int IsWatermark = 0)
        {
            System.Drawing.Bitmap Tn_image = null;
            System.Drawing.Bitmap Show_image = null;
            System.Drawing.Image original_image = null;
            try
            {
                HttpServerUtility Server = HttpContext.Current.Server;
                original_image = System.Drawing.Image.FromStream(image_upload.InputStream);
                UserPictureEntity entity = null;
                #region 如果是横向图片，则进行旋转操作
                try
                {
                    ImageExif exif = ImageHelper.ExifInfo(original_image);
                    string gps_Longitude = string.IsNullOrEmpty(exif.GPSLongitude) ? "" : exif.GPSLongitude;
                    string gps_Latitude = string.IsNullOrEmpty(exif.GPSLatitude) ? "" : exif.GPSLatitude;
                    //如果是横向图片，则进行旋转操作
                    if (exif.Orientation != null)
                    {
                        if (exif.Orientation == "6")
                        {
                            original_image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                        }
                        else if (exif.Orientation == "8")
                        {
                            original_image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                        }
                        else if (exif.Orientation == "3")
                        {
                            original_image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                        }
                    }

                }
                catch { }
                #endregion

                string OrgiFileName = image_upload.FileName;
                string Title = System.IO.Path.GetFileNameWithoutExtension(OrgiFileName);
                string newFimeName = UICommon.Util.GetRandNumber(15).ToLower();//新文件名称，随机15位数字 
                string extension = System.IO.Path.GetExtension(image_upload.FileName);//如.gif

                int UserPictureID = 0;

                #region 保存原图 orig
                string fileName = "orig_" + newFimeName + extension;//如orig_xxxxx.jpg
                //获取相对地址/upload/10/ContentPictures/2010/10/2/orig_xxxxx.jpg
                string imgsrcorig = UICommon.FileHelper.GetFilePath(UserID, UICommon.FileNameIndex.Picture, fileName);
                //保存原图image_upload

                #region 加水印
                if (IsWatermark == 1)
                {
                    AddWatermark(original_image, UserID);
                }
                #endregion

                original_image.Save(Server.MapPath(imgsrcorig));  //保存原图 

                //其他几个图 微缩图  Tn    Show  Orig 
                int tn_width = 0;
                int tn_height = 100;
                if (original_image.Width > original_image.Height)
                {
                    tn_width = 100;
                    tn_height = 0;
                }

                Tn_image = ImageHelper.GetThumbnailImage(original_image, ThumbnailImageType.Zoom, tn_width, tn_height);
                string Tn_pictureName = "tn_" + newFimeName + extension;
                string Tn_imgsrc = UICommon.FileHelper.GetFilePath(UserID, UICommon.FileNameIndex.Picture, Tn_pictureName);
                Tn_image.Save(Server.MapPath(Tn_imgsrc));

                Show_image = ImageHelper.GetThumbnailImage(original_image, ThumbnailImageType.Zoom, 600, 0);
                string Show_pictureName = "show_" + newFimeName + extension;
                string Show_imgsrc = UICommon.FileHelper.GetFilePath(UserID, UICommon.FileNameIndex.Picture, Show_pictureName);
                Show_image.Save(Server.MapPath(Show_imgsrc));

                float origFileSize = FileHelper.GetFileSize(Server.MapPath(imgsrcorig));//图片存放空间大小
                if (origFileSize > 0)
                {
                    #region 保存到数据库

                    #region 处理相册

                    if (BookID > 0)
                    {
                        //检测是否存在相册
                        Model.UserPictureBookEntity bookEntity = DAL.UserPictureBookDAL.Get_99(BookID, "ID");
                        if (bookEntity == null || bookEntity.ID <= 0)
                        {
                            BookID = 0;
                        }
                    }
                    if (BookID <= 0)
                    {
                        //检测是否存在
                        BookID = DAL.UserPictureBookDAL.GetSingle("ID", "UserID=" + UserID);
                        if (BookID <= 0)
                        {
                            //不存在图片册则新建一个默认的
                            SqlParameter[] pramsAdd =
                            {
                                DAL.DALUtil.MakeInParam("@Name",System.Data.SqlDbType.NVarChar,200,"默认相册"),
                                DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,UserID),
                                DAL.DALUtil.MakeInParam("@OrderNum",System.Data.SqlDbType.Int,4,1),      
                                DAL.DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,DateTime.Now),  
                            };
                            BookID = DAL.UserPictureBookDAL.Add(pramsAdd);
                        }
                    }
                    if (BookID <= 0)
                    {
                        return null;
                        //创建失败
                    }
                    #endregion

                    #region 保存图片

                    SqlParameter[] pramsAdd2 =
                            {
                                DAL.DALUtil.MakeInParam("@Title",System.Data.SqlDbType.NVarChar,100, Title),//Title
                                DAL.DALUtil.MakeInParam("@FileName",System.Data.SqlDbType.NVarChar,250, OrgiFileName),
                                DAL.DALUtil.MakeInParam("@UserID",System.Data.SqlDbType.Int,4,UserID),
                                DAL.DALUtil.MakeInParam("@BookID",System.Data.SqlDbType.Int,4,BookID), 
                                DAL.DALUtil.MakeInParam("@UseCount",System.Data.SqlDbType.Int,4,0),//使用次数
                                DAL.DALUtil.MakeInParam("@CreateTS",System.Data.SqlDbType.DateTime,8,DateTime.Now),  

                                DAL.DALUtil.MakeInParam("@Tn",System.Data.SqlDbType.NVarChar,250, Tn_imgsrc),
                                DAL.DALUtil.MakeInParam("@Show",System.Data.SqlDbType.NVarChar,250, Show_imgsrc),
                                DAL.DALUtil.MakeInParam("@Orig",System.Data.SqlDbType.NVarChar,250, imgsrcorig ), 

                                DAL.DALUtil.MakeInParam("@Size",System.Data.SqlDbType.Float,10, origFileSize ), 
                                DAL.DALUtil.MakeInParam("@Extension",System.Data.SqlDbType.NVarChar,50, extension ), 
                            };
                    UserPictureID = DAL.UserPictureDAL.Add(pramsAdd2);
                    if (UserPictureID > 0)
                    {
                        entity = DAL.UserPictureDAL.Get_99(UserPictureID, "*");
                    }
                    else
                    {
                        return null;
                        //保存失败
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    return null;
                }
                #endregion

                #region 水印

                //watermark._watermark _water = watermark.watermarklist().Find(a => a.CityCode == CurrentCity.CityCode);
                //string waterMark = _water != null ? _water.ImgUrl : "/images/mark_logo.png";
                //ImageHelper.AddImageSignPic(HttpContext.Current, imgsrcshow, imgsrcshow, waterMark, 9, 100, 10);

                #endregion

                return entity;
            }
            catch (Exception ex)
            {
                LogCommon.Logs.write("upload_Error", ex.ToString());
                return null;
            }
            finally
            {
                if (original_image != null) original_image.Dispose();
                if (Show_image != null) Show_image.Dispose();
                if (Tn_image != null) Tn_image.Dispose();
            }
        }

        private static void AddWatermark(System.Drawing.Image image, int UserID)
        {
            Graphics graph = Graphics.FromImage(image);
            Model.UserWatermarkEntity entity = DAL.UserWatermarkDAL.Get_98(UserID, "*");
            if (entity != null && entity.ID > 0)
            {
                FontStyle _fontStyle = (FontStyle)UICommon.Util.ConvertToInt32(entity.FontStyle);//?? FontStyle.Bold;
                float _size = UICommon.Util.ConvertToInt32(entity.WmkSize);// 16; 
                string rColor = entity.WmkColor;
                float _alpha = Convert.ToSingle(Convert.ToInt32(entity.WmkAlpha) / 100F);// 16;
                Color color = WatermarkUtil.ToColor(rColor, _alpha);
                UICommon.WatermarkUtil.addWatermarkText(graph, entity.WmkText, entity.WmkPosition, image.Width, image.Height, color, entity.FamilyName, _fontStyle, _size);
            }
            else
            {
                //entity = new UserWatermarkEntity();
                //entity.WmkType = 0;
                //entity.UserID = 0;
                //entity.WmkPosition = 0;
                //entity.FamilyName = 0;
                //entity.FontStyle = 0;
                //entity.WmkSize = 0;
                //entity.WmkText = 0;
                //entity.WmkColor = 0;
                //entity.WmkSizePercent = 0;
                //entity.WmkAlpha = 0;
            }
        }

        #region  删除对应的文件夹PictureID 必须存在的
        public static void MoveFile(int PictureID, int UserID)
        {
            //
            Model.UserPictureEntity entity = DAL.UserPictureDAL.Get_99(PictureID, "*");
            if (entity != null && entity.ID > 0)
            {
                HttpServerUtility Server = HttpContext.Current.Server;

                string tn = System.IO.Path.GetFileName(entity.Tn);
                string sourceFileName = Server.MapPath(entity.Tn);
                string destFileName = GetTempServerPathName(UserID, tn);
                File.Move(sourceFileName, destFileName);

                string Show = System.IO.Path.GetFileName(entity.Show);
                sourceFileName = Server.MapPath(entity.Show);
                destFileName = GetTempServerPathName(UserID, Show);
                File.Move(sourceFileName, destFileName);


                string Orig = System.IO.Path.GetFileName(entity.Orig);
                sourceFileName = Server.MapPath(entity.Orig);
                destFileName = GetTempServerPathName(UserID, Orig);
                File.Move(sourceFileName, destFileName);

            }

        }

        private static string GetTempServerPathName(int UserID, string name)
        {
            string dir = string.Empty;
            try
            {
                string file = FileNameIndex.DelPictures.ToString();
                string pathFile = "/upload/" + UserID + "/" + file + "/";
                string serverPath = HttpContext.Current.Server.MapPath(pathFile);
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                serverPath += name;
                dir = serverPath;
            }
            catch (Exception)
            {

                throw;
            }
            return dir;
        }
        #endregion


        #region 文件上传成功后获取图片的Exif

        /// <summary>
        /// 获取图片的EXIF信息
        /// </summary>
        /// <param name="filePath"></param>
        private static string GetImageEXIFInfo(string strFileName, out Hashtable htExif)
        {
            string strResultKey = null, strResultValue = null;
            Hashtable _htExif = new Hashtable();
            using (var reader = new ExifReader(strFileName))
            {
                // Parse through all available fields and generate key-value labels 
                var props = Enum.GetValues(typeof(ExifTags)).Cast<ushort>().Select(tagID =>
                {
                    object val;
                    if (reader.GetTagValue(tagID, out val))
                    {
                        // Special case - some doubles are encoded as TIFF rationals. These
                        // items can be retrieved as 2 element arrays of {numerator, denominator}
                        if (val is double)
                        {
                            int[] rational;
                            if (reader.GetTagValue(tagID, out rational))
                                val = string.Format("{0} ({1}/{2})", val, rational[0], rational[1]);
                        }

                        strResultKey = Enum.GetName(typeof(ExifTags), tagID);
                        strResultValue = RenderTag(val);

                        if (strResultKey.Equals("Make") || strResultKey.Equals("Model") || strResultKey.Equals("ExposureTime")
                            || strResultKey.Equals("FNumber") || strResultKey.Equals("DateTimeDigitized") || strResultKey.Equals("FocalLength")
                            || strResultKey.Equals("ISOSpeedRatings"))
                        {
                            _htExif.Add(strResultKey, strResultValue);
                        }

                        return string.Format("{0}: {1}", strResultKey, strResultValue);
                    }

                    return null;

                }).Where(x => x != null).ToArray();

                htExif = _htExif;

                return string.Join("|", props);
            }

        }

        private static string RenderTag(object tagValue)
        {
            // Arrays don't render well without assistance.
            var array = tagValue as Array;
            if (array != null)
                return string.Join(", ", array.Cast<object>().Select(x => x.ToString()).ToArray());

            return tagValue.ToString();
        }

        #endregion
    }
}
