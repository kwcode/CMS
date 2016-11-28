using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：DomainEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-28 00:04
    /// </summary>
    public class DomainEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public string DomainName { get; set; }
        public int OrderNum { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

