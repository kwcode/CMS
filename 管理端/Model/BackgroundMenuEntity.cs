using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：BackgroundMenuEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-27 00:55
    /// </summary>
    public class BackgroundMenuEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int ValueNum { get; set; }
        public string Title { get; set; }
        public int OrderNum { get; set; }
        public string Description { get; set; }
        public int BackgroundMenuClass1_ValueNum { get; set; }
        public string ManageUrl { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}


