using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Yank.WeiXin.Robot.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class Message : ITemplate
    {
        /// <summary>
        /// 发送方帐号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 接收方账号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; protected set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public virtual string Template
        {
            get { throw new NotImplementedException(); }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public Message()
        { }
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateContent()
        {
            throw new NotImplementedException();
        }
    }
}
