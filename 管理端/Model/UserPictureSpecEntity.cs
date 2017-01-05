using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：UserPictureSpecEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-09 23:58
    /// </summary>
    public class UserPictureSpecEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public DateTime CreateTS { get; set; }
        public int ValueNum { get; set; }
        public int SystemPictureSpec_ValueNum { get; set; }
        /// <summary>
        /// 图片类型
        /// 小图small  中图show 大图big 原图orig
        /// </summary>
        public string PictureType { get; set; }
        #endregion

    }

}

