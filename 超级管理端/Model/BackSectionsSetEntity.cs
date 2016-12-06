using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// <summary>
    /// 实体：BackSectionsSetEntity
    /// 创建工具 :TCode
    /// 生成时间:2016-12-02 22:18
    /// </summary>
    public class BackSectionsSetEntity
    {
        #region 原始字段

        public int ID { get; set; }
        public string Title { get; set; }
        public int ValueNum { get; set; }
        public int OrderNum { get; set; }
        public string Description { get; set; }
        public DateTime CreateTS { get; set; }
        /// <summary>
        /// 管理链接
        /// </summary>
        public string ManageUrl { get; set; }
        #endregion

    }
}

