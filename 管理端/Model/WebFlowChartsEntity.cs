using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：WebFlowChartsEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-25 17:00
    /// </summary>
    public class WebFlowChartsEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int IPCount { get; set; }
        public int PVCount { get; set; }
        public string DTime { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion

    }
}

