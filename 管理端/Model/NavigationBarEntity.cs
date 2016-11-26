using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类NavigationBar
    /// Class Name：NavigationBar-对应实体类
    /// Depiction：NavigationBar-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-25 00:15
    /// </summary>
    public class NavigationBarEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public int OrderNum { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int ValueNum { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

