using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Yank.WeiXin.Robot.Handlers
{
    /// <summary>
    /// 处理器工厂类
    /// </summary>
    public class HandlerFactory
    {
        /// <summary>
        /// 创建处理器
        /// </summary>
        /// <param name="requestXml">请求的xml</param>
        /// <returns>IHandler对象</returns>
        public static IHandler CreateHandler(string requestXml)
        {
            IHandler handler = null;
            if (!string.IsNullOrEmpty(requestXml))
            {
                //解析数据
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(requestXml);
                XmlNode node = doc.SelectSingleNode("/xml/MsgType");
                if (node != null)
                {
                    XmlCDataSection section = node.FirstChild as XmlCDataSection;
                    if (section != null)
                    {
                        string msgType = section.Value;

                        switch (msgType)
                        {
                            case "text":
                                handler = new TextHandler(requestXml);
                                break;
                            case "event":
                                handler = new EventHandler(requestXml);
                                break;
                        }
                    }
                }
            }

            return handler;
        }
    }
}
