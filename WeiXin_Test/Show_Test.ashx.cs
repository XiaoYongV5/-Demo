using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yank.WeiXin.Robot;

namespace WeiXin_Test
{
	/// <summary>
	/// Show_Test 的摘要说明
	/// </summary>
	public class Show_Test : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			//context.Response.Write("Hello World");

			//由微信服务接收请求，具体处理请求
			WeiXinService wxService = new WeiXinService(context.Request);
			string responseMsg = wxService.Response();
			context.Response.Clear();
			context.Response.Charset = "UTF-8";
			context.Response.Write(responseMsg);
			context.Response.End();

		}
		
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}