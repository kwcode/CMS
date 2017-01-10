using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：UserPictureEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-10 21:12
    /// </summary>
    public class UserPictureEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public DateTime CreateTS { get; set; }
        public int UseCount { get; set; }
        public float Size { get; set; }
        public string Extension { get; set; }
        /// <summary>
        /// 微缩图200*200
        /// </summary>
        public string Tn { get; set; }
        /// <summary>
        /// 图500*
        /// </summary>
        public string Show { get; set; }
        /// <summary>
        /// 原图
        /// </summary>
        public string Orig { get; set; }

        #endregion

    }
}

