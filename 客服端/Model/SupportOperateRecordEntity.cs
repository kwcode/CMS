using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：SupportOperateRecordEntity
    /// 创建工具 :TCode
    /// 生成时间:2017-01-12 10:32
    /// </summary>
    public class SupportOperateRecordEntity
    {
        #region 原始字段

        public int KeyID { get; set; }
        public int SupportID { get; set; }
        public int ManagerID { get; set; }
        public DateTime AddTime { get; set; }
        public int TypeID { get; set; }
        public string Detail { get; set; }

        #endregion

    }
}

