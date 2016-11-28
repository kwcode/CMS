using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：OnlineMessageEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-25 23:13
    /// </summary>
    public class OnlineMessageEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public int OrderNum { get; set; }
        public string TxtContent { get; set; }
        public DateTime CreateTS { get; set; }
        public int IsReaded { get; set; }
        public string IP { get; set; }
        public string Contacts { get; set; }

        #endregion

    }
}

