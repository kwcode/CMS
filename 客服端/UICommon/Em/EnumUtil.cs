using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UICommon.Em
{
    public class EnumUtil
    {
        #region 枚举添加
        /// <summary>
        /// 添加枚举
        /// </summary>
        /// <param name="Title"></param> 
        private static BaseEnum AddEnum(string Title, int ValueNum, string ValueTxt)
        {
            BaseEnum entity = new BaseEnum();
            entity.Title = Title;
            entity.ValueNum = ValueNum;
            entity.ValueTxt = ValueTxt;
            return entity;
        }
        #endregion
        #region 在线客服 枚举
        /// <summary>
        /// 在线客服 枚举
        /// </summary>
        /// <returns></returns>
        public static List<BaseEnum> GetOSList()
        {
            List<BaseEnum> list = new List<BaseEnum>();
            list.Add(AddEnum("QQ", 1, ""));
            list.Add(AddEnum("微信", 2, ""));
            list.Add(AddEnum("淘宝旺旺", 3, ""));
            list.Add(AddEnum("MSN", 4, ""));
            list.Add(AddEnum("邮箱", 5, ""));
            return list;
        }
        /// <summary>
        /// 获取在线客服类型 中文显示名称
        /// </summary> 
        public static string GetOSTitle(int ValueNum)
        {
            string str = string.Empty;
            try
            {
                List<BaseEnum> list = GetOSList();
                BaseEnum entity = list.Find(o => { return o.ValueNum == ValueNum; });
                return entity.Title;
            }
            catch (Exception ex)
            {
            }
            return str;
        }

        #endregion
    }
}
