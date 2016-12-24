using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：OnlineServicesEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-25 00:09
    /// </summary>
    public class OnlineServicesEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int OSType { get; set; }
        public int UserID { get; set; }
        public int OrderNum { get; set; }
        public string Title { get; set; }
        public string ValueNum { get; set; }
        public DateTime CreateTS { get; set; }
        public string Remark { get; set; }

        #endregion

    }
}

