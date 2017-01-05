using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：UserInfoExtraEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-28 23:53
    /// </summary>
    public class UserInfoExtraEntity
    {
        #region 原始字段

        public int UserID { get; set; }
        /// <summary>
        /// 是否开通在线客服
        /// </summary>
        public int IsOnlineServices { get; set; }
        /// <summary>
        /// 自定义在线客服JS
        /// </summary>
        public string OSJsCode { get; set; }
        /// <summary>
        /// 是否开启流量监控
        /// </summary>
        public int IsIpTongJi { get; set; }
        public string TJJsCode { get; set; }
        #endregion

    }
}

