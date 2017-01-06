using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using Yank.WeiXin.Robot.Handlers;
using Yank.WeiXin.Robot.Utility;


namespace Yank.WeiXin.Robot
{
    public class WeiXinService
    {
        /// <summary>
        /// TOKEN
        /// </summary>
        private const string TOKEN = "weixin";
        /// <summary>
        /// 签名
        /// </summary>
        private const string SIGNATURE = "signature";
        /// <summary>
        /// 时间戳
        /// </summary>
        private const string TIMESTAMP = "timestamp";
        /// <summary>
        /// 随机数
        /// </summary>
        private const string NONCE = "nonce";
        /// <summary>
        /// 随机字符串
        /// </summary>
        private const string ECHOSTR = "echostr";     
        /// <summary>
        /// 
        /// </summary>
        private HttpRequest Request { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request"></param>
        public WeiXinService(HttpRequest request)
        {
            this.Request = request;
        }
        /// <summary>
        /// 处理请求，产生响应
        /// </summary>
        /// <returns></returns>
        public string Response()
        {
            string method = Request.HttpMethod.ToUpper();
            //验证签名
            if (method == "GET")
            {
                if (CheckSignature())
                {
                    return Request.QueryString[ECHOSTR];
                }
                else
                {
                    return "error";
                }
            }

            //处理消息
            if (method == "POST")
            {
                return ResponseMsg();
            }
            else
            {
                return "无法处理";
            }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        private string ResponseMsg()
        {
            string requestXml = Common.ReadRequest(this.Request);
            IHandler handler = HandlerFactory.CreateHandler(requestXml);
            if (handler != null)
            {
                return handler.HandleRequest();
            }

            return string.Empty;
        }
        /// <summary>
        /// 检查签名
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString[SIGNATURE];
            string timestamp = Request.QueryString[TIMESTAMP];
            string nonce = Request.QueryString[NONCE];

            List<string> list = new List<string>();
            list.Add(TOKEN);
            list.Add(timestamp);
            list.Add(nonce);
            //排序
            list.Sort();
            //拼串
            string input = string.Empty;
            foreach (var item in list)
            {
                input += item;
            }
            //加密
            string new_signature = SecurityUtility.SHA1Encrypt(input);
            //验证
            if (new_signature == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
