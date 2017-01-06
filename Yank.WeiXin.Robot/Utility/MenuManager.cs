using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Yank.WeiXin.Robot.Utility
{
	public class MenuManager
	{
		/// <summary>
		/// 菜单文件路径
		/// </summary>
		private static readonly string Menu_Data_Path = System.AppDomain.CurrentDomain.BaseDirectory + "/Data/menu.txt";
		/// <summary>
		/// 获取菜单
		/// </summary>
		public static string GetMenu()
		{
			string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", Context.AccessToken);

			return HttpUtility.GetData(url);
		}
		/// <summary>
		/// 创建菜单
		/// </summary>
		public static void CreateMenu(string menu)
		{
			string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", Context.AccessToken);
			//string menus = FileUtility.Read(Menu_Data_Path);
			HttpUtility.SendHttpRequest(url, menu);
		}
		/// <summary>
		/// 删除菜单
		/// </summary>
		public static void DeleteMenu()
		{
			string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", Context.AccessToken);
			HttpUtility.GetData(url);
		}
		/// <summary>
		/// 加载菜单
		/// </summary>
		/// <returns></returns>
		public static string LoadMenu()
		{
			return FileUtility.Read(Menu_Data_Path);
		}
	}
}
