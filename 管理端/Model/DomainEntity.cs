using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类Domain
    /// Class Name：Domain-对应实体类
    /// Depiction：Domain-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-25 00:15
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

