using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：SuperAdministratorEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-11-26 00:13
    /// </summary>
    [Serializable]
    public class SuperAdministratorEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public string NickName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public DateTime? CreateTS { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLoginIp { get; set; }
        public int UserRole { get; set; }

        #endregion

        #region 额外的
        /// <summary>
        /// 返回结果
        /// </summary>
        public int Result { get; set; }
        #endregion

    }
}

