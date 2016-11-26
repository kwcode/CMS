using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类Banner
    /// Class Name：Banner-对应实体类
    /// Depiction：Banner-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-25 00:14
    /// </summary>
    public class BannerEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public int OrderNum { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

