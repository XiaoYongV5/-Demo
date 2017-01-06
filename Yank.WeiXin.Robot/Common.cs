using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Yank.WeiXin.Robot
{
    /// <summary>
    /// 公共功能
    /// </summary>
    class Common
    {
        /// <summary>
        /// 加密签名
        /// </summary>
        public const string SIGNATURE = "signature";
        /// <summary>
        /// 时间戳
        /// </summary>
        public const string TIMESTAMP = "timestamp";
        /// <summary>
        /// 随机数
        /// </summary>
        public const string NONCE = "nonce";
        /// <summary>
        /// 随机字符串
        /// </summary>
        public const string ECHOSTR = "echostr";

        /// <summary>
		/// 发送人（OpenID）
        /// </summary>
        public const string FROM_USERNAME = "FromUserName";
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public const string TO_USERNAME = "ToUserName";
        /// <summary>
        /// 消息内容
        /// </summary>
        public const string CONTENT = "Content";
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public const string CREATE_TIME = "CreateTime";
        /// <summary>
        /// 消息类型
        /// </summary>
        public const string MSG_TYPE = "MsgType";
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public const string MSG_ID = "MsgId";
        /// <summary>
        /// 得到当前时间（整型）（考虑时区）
        /// </summary>
        /// <returns></returns>
        public static string GetNowTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
            return a.ToString();
        }
        /// <summary>
        /// 读取请求对象的内容
        /// 只能读一次
        /// </summary>
        /// <param name="request">HttpRequest对象</param>
        /// <returns>string</returns>
        public static string ReadRequest(HttpRequest request)
        {
            string reqStr = string.Empty;
            using (Stream s = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                {
                    reqStr = reader.ReadToEnd();
                }
            }

            return reqStr;
        }
    }
}
