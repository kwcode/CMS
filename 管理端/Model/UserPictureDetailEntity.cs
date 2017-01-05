using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：UserPictureDetailEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-10 13:26
    /// </summary>
    public class UserPictureDetailEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserPicture_ID { get; set; }
        public int UserID { get; set; }
        public string PictureType { get; set; }
        public string ImagePath { get; set; }
        public float Size { get; set; }
        public string Extension { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

