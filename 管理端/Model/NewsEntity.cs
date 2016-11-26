using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：NewsEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-27 01:31
    /// </summary>
    public class NewsEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Summay { get; set; }
        public string TitlePictures { get; set; }
        public int NewsClass1_ID { get; set; }
        public string TxtContent { get; set; }
        public int OrderNum { get; set; }
        public int ValueNum { get; set; }
        public int ClickRate { get; set; }
        public int ClickZan { get; set; }
        public DateTime? MaturityDate { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

