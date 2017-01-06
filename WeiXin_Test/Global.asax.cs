using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeiXin_Test.Services;

namespace WeiXin_Test
{
	// 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
	// 请访问 http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

		}
		

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError().GetBaseException();
			StringBuilder str = new StringBuilder();
			str.Append("\r\n.客户信息：");
			string ip = "";
			if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
			{
				ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
			}
			else
			{
				ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
			}
			str.Append("\r\n\tIp:" + ip);
			str.Append("\r\n\t浏览器:" + Request.Browser.Browser.ToString());
			str.Append("\r\n\t浏览器版本:" + Request.Browser.MajorVersion.ToString());
			str.Append("\r\n\t操作系统:" + Request.Browser.Platform.ToString());
			str.Append("\r\n.错误信息：");
			str.Append("\r\n\t页面：" + Request.Url.ToString());
			str.Append("\r\n\t错误信息：" + ex.Message);
			str.Append("\r\n\t错误源：" + ex.Source);
			str.Append("\r\n\t异常方法：" + ex.TargetSite);
			str.Append("\r\n\t堆栈信息：" + ex.StackTrace);
			str.Append("\r\n--------------------------------------------------------------------------------------------------");
			//创建路径 
			LogWriter.Default.WriteError(str.ToString());
		}
	}
}