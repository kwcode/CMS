using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类ProductImages
    /// Class Name：ProductImages-对应实体类
    /// Depiction：ProductImages-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-19 01:45
    /// </summary>
    public class ProductImagesEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int Product_ID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int OrderNum { get; set; }
        public DateTime? AddDate { get; set; }
        public int ProductImageStatus { get; set; }

        #endregion

    }
}

