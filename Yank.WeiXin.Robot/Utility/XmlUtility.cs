using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Yank.WeiXin.Robot.Utility
{
	class XmlUtility
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="rootName"></param>
		/// <returns></returns>
		public static XDocument ParseJson(string json, string rootName)
		{
			return JsonConvert.DeserializeXNode(json, rootName);
		}
	}
}
