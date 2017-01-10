using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UICommon.Em
{
    public class BaseEnum
    {
        /// <summary>
        /// 枚举显示值
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 枚举值
        /// </summary>
        public int ValueNum { get; set; }
        /// <summary>
        /// 枚举中文值
        /// </summary>
        public string ValueTxt { get; set; }
    }
}
