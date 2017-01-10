using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：UserWatermarkEntity
    /// 创建工具 :TCode
    /// 生成时间:2017-01-02 03:11
    /// </summary>
    public class UserWatermarkEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public int UserID { get; set; }
        public int WmkType { get; set; }
        public int WmkPosition { get; set; }
        public string FamilyName { get; set; }
        public int FontStyle { get; set; }
        public int WmkSize { get; set; }
        public string WmkText { get; set; }
        public string WmkColor { get; set; }
        public int WmkSizePercent { get; set; }
        public int WmkAlpha { get; set; }

        #endregion

    }
}

