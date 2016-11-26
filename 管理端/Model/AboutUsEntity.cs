using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：AboutUsEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-25 23:11
    /// </summary>
    public class AboutUsEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int ValueNum { get; set; }
        public string Title { get; set; }
        public int OrderNum { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string TxtContent { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

