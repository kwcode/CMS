using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：WebFlowTongJiEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-25 15:16
    /// </summary>
    public class WebFlowTongJiEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime CreateTS { get; set; }
        public string IP { get; set; }
        public string IPSourceUrl { get; set; }
        public string BrowseUrl { get; set; }

        #endregion

    }
}

