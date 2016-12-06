using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：UserInfoEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-27 23:36
    /// </summary>
    [Serializable]
    public class UserInfoEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public string UserName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }
        public string TC_Name { get; set; }
        public DateTime? MaturityTime { get; set; }
        public int Templates_ValueNum { get; set; }
        public DateTime CreateTS { get; set; }

        #endregion
         
        public string LoginName { get; set; }
    }
}

