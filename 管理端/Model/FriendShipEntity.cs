using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：FriendShipEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-27 18:11
    /// </summary>
    public class FriendShipEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public int OrderNum { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

