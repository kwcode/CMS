using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类PictureText
    /// Class Name：PictureText-对应实体类
    /// Depiction：PictureText-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-25 00:26
    /// </summary>
    public class PictureTextEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string Url { get; set; }
        public string TxtContent { get; set; }
        public int OrderNum { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int ValueNum { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

