using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：ArticleEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-27 02:32
    /// </summary>
    public class ArticleEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int ArticleClass1_ValueNum { get; set; }
        public int ValueNum { get; set; }
        public string Title { get; set; }
        public string Summay { get; set; }
        public string TitlePictures { get; set; }
        public string TxtContent { get; set; }
        public int OrderNum { get; set; }
        public string Description { get; set; }
        public int ClickRate { get; set; }
        public int ClickZan { get; set; }
        public DateTime? MaturityDate { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

