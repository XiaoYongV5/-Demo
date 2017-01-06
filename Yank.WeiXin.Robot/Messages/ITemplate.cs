using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yank.WeiXin.Robot.Messages
{
    /// <summary>
    /// 模板接口
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// 模板
        /// </summary>
        string Template { get;}
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns>string</returns>
        string GenerateContent();
    }
}
