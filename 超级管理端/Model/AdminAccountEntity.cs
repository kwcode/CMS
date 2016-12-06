using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：AdminAccountEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-28 21:55
    /// </summary>
    public class AdminAccountEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

