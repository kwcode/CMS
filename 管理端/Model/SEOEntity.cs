using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：SEOEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-25 14:57
    /// </summary>
    public class SEOEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

