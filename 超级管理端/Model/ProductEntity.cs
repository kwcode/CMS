using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类Product
    /// Class Name：Product-对应实体类
    /// Depiction：Product-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-19 01:44
    /// </summary>
    public class ProductEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductClass1_ID { get; set; }
        public int ProductClass2_ID { get; set; }
        public int ProductClass3_ID { get; set; }
        public string Title { get; set; }
        public int ProductStatus { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal MemberPrice { get; set; }
        public string Summay { get; set; }
        public string SeoTitle { get; set; }
        public string TxtContent { get; set; }
        public string Units { get; set; }
        public int ProductNum { get; set; }
        public DateTime? AddDate { get; set; }
        public int OrderNum { get; set; }
        public int ClickRate { get; set; }
        public int ClickLike { get; set; }
        public string TitlePictures { get; set; }
        #endregion

    }
}

