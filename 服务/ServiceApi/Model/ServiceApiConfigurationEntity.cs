using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：ServiceApiConfigurationEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-02 21:01
    /// </summary>
    public class ServiceApiConfigurationEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public string ServiceUrl { get; set; }
        public string ServiceName { get; set; }
        public int ValueNum { get; set; }
        public DateTime CreateTS { get; set; }
        public string Token { get; set; }

        #endregion

    }
}

