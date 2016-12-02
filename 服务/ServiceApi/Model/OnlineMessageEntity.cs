using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：OnlineMessageEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-01 22:38
    /// </summary>
    public class OnlineMessageEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public int OrderNum { get; set; }
        public string TxtContent { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string QQ { get; set; }
        public string Contacts { get; set; }
        public int IsReaded { get; set; }
        public string IP { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

