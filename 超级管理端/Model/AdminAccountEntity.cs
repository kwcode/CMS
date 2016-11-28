using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体类AdminAccount
    /// Class Name：AdminAccount-对应实体类
    /// Depiction：AdminAccount-对应实体类
    /// Create By :TCode
    /// Create Date:2016-11-13 23:05
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

