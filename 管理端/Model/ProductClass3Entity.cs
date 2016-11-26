using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类ProductClass3
    /// Class Name：ProductClass3-对应实体类
    /// Depiction：ProductClass3-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-19 01:46
    /// </summary>
    public class ProductClass3Entity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductClass1_ID { get; set; }
        public int ProductClass2_ID { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int OrderNum { get; set; }
        public DateTime? AddDate { get; set; }

        #endregion

    }
}

