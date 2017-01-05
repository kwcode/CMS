using System;
namespace Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public class UserInfoEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public string UserName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime AddDate { get; set; }
        public string TC_Name { get; set; }
        /// <summary>
        /// 是否IP统计
        /// </summary>
        public int IsIpTongJi { get; set; }
        #endregion

        public string LoginName { get; set; }
    }

}
