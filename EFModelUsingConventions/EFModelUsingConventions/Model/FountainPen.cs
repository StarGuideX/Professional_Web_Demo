using System;
using System.Collections.Generic;
using System.Text;

namespace EFModelUsingConventions.Model
{
    /// <summary>
    /// 刚笔
    /// </summary>
    public class FountainPen:Pen
    {
        /// <summary>
        /// 刚笔墨囊
        /// </summary>
        public InkSac InkSac { get; set; }
    }
}
