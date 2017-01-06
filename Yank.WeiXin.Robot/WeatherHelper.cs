using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using System.Xml;

namespace Yank.WeiXin.Robot
{
    class WeatherHelper
    {
        /// <summary>
        /// 城市集合字段
        /// </summary>
        private static Dictionary<string, City> mCities;
      
		/// <summary>
        /// 城市集合
        /// </summary>
		public static Dictionary<string, City> Cities
        {
            get
            {
                if (mCities == null)
                {
                    LoadCities();
                }

                return mCities;
            }
        }
       
		/// <summary>
        /// 加载城市
        /// </summary>
        private static void LoadCities()
        {
            mCities = new Dictionary<string, City>();
            mCities.Clear();
            mCities.Add("101010100", new City() { Code = "101010100", Name = "北京", PinYin = "beijing", FristLetter = "bj" });
            mCities.Add("101020100", new City() { Code = "101020100", Name = "上海", PinYin = "shanghai", FristLetter = "sh" });
            mCities.Add("101200101", new City() { Code = "101200101", Name = "武汉", PinYin = "wuhai", FristLetter = "wh" });
            
        }
     
		/// <summary>
        /// 获取城市的天气
        /// </summary>
        /// <param name="name">城市名称、拼音或首字母</param>
        /// <returns></returns>
        public static string GetWeather(string name)
        {
            string result = string.Empty;
            string cityCode = string.Empty;
            //获取城市编码
            IEnumerable<string> codes = from item in Cities
                                        where item.Value != null
                                              && (item.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase) 
                                                    || item.Value.PinYin.Equals(name, StringComparison.OrdinalIgnoreCase) 
                                                    || item.Value.FristLetter.Equals(name, StringComparison.OrdinalIgnoreCase))
                                        select item.Value.Code;
            if (codes != null && codes.Count() > 0)
            {
                cityCode = codes.First<string>();
            }

            //http请求，获取天气
            if (!string.IsNullOrEmpty(cityCode))
            {
				string url = "http://m.weather.com.cn/mweather/{0}.shtml";
                url = string.Format(url, cityCode);
                WebRequest request = HttpWebRequest.Create(url);
                //超时时间为：2秒
                request.Timeout = 20000;
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string weahterInfo = reader.ReadToEnd();
                if (string.IsNullOrEmpty(weahterInfo))
                {
                    result = "暂时没有取到天气数据,请稍后再试";
                }
                else
                {
					try
					{
						XmlDocument doc = JsonConvert.DeserializeXmlNode(weahterInfo);
						if (doc != null)
						{
							XmlNode node = doc.DocumentElement;
							if (node != null)
							{
								StringBuilder builder = new StringBuilder();
								builder.Append(node["city"].InnerText).Append("\n");
								builder.Append(node["date_y"].InnerText).Append(" ").Append(node["week"].InnerText).Append("\n");
								builder.Append("今天：").Append("(").Append(node["temp1"].InnerText).Append(")").Append(node["weather1"].InnerText).Append(node["wind1"].InnerText).Append(node["fl1"].InnerText).Append("\n");
								builder.Append("24小时穿衣指数:").Append(node["index_d"].InnerText).Append("\n");
								builder.Append("明天：").Append("(").Append(node["temp2"].InnerText).Append(")").Append(node["weather2"].InnerText).Append(node["wind2"].InnerText).Append(node["fl2"].InnerText).Append("\n");
								builder.Append("48小时穿衣指数:").Append(node["index48_d"].InnerText).Append("\n");
								result = builder.ToString();
							}
						}
						#region 天气json数据格式
						/*
                 
                {
   "weatherinfo": {
       "city": "北京", 
       "city_en": "beijing", 
       "date_y": "2013年11月4日", 
       "date": "", 
       "week": "星期一", 
       "fchh": "11", 
       "cityid": "101010100", 
       "temp1": "17℃~5℃", 
       "temp2": "16℃~5℃", 
       "temp3": "18℃~4℃", 
       "temp4": "17℃~5℃", 
       "temp5": "14℃~6℃", 
       "temp6": "14℃~2℃", 
       "tempF1": "62.6℉~41℉", 
       "tempF2": "60.8℉~41℉", 
       "tempF3": "64.4℉~39.2℉", 
       "tempF4": "62.6℉~41℉", 
       "tempF5": "57.2℉~42.8℉", 
       "tempF6": "57.2℉~35.6℉", 
       "weather1": "晴转多云", 
       "weather2": "多云", 
       "weather3": "多云转晴", 
       "weather4": "晴转多云", 
       "weather5": "多云转阴", 
       "weather6": "阴转晴", 
       "img1": "0", 
       "img2": "1", 
       "img3": "1", 
       "img4": "99", 
       "img5": "1", 
       "img6": "0", 
       "img7": "0", 
       "img8": "1", 
       "img9": "1", 
       "img10": "2", 
       "img11": "2", 
       "img12": "0", 
       "img_single": "0", 
       "img_title1": "晴", 
       "img_title2": "多云", 
       "img_title3": "多云", 
       "img_title4": "多云", 
       "img_title5": "多云", 
       "img_title6": "晴", 
       "img_title7": "晴", 
       "img_title8": "多云", 
       "img_title9": "多云", 
       "img_title10": "阴", 
       "img_title11": "阴", 
       "img_title12": "晴", 
       "img_title_single": "晴", 
       "wind1": "微风", 
       "wind2": "微风", 
       "wind3": "微风", 
       "wind4": "微风", 
       "wind5": "微风", 
       "wind6": "北风4-5级", 
       "fx1": "微风", 
       "fx2": "微风", 
       "fl1": "小于3级", 
       "fl2": "小于3级", 
       "fl3": "小于3级", 
       "fl4": "小于3级", 
       "fl5": "小于3级", 
       "fl6": "4-5级", 
       "index": "较冷", 
       "index_d": "建议着大衣、呢外套加毛衣、卫衣等服装。体弱者宜着厚外套、厚毛衣。因昼夜温差较大，注意增减衣服。", 
       "index48": "冷", 
       "index48_d": "天气冷，建议着棉服、羽绒服、皮夹克加羊毛衫等冬季服装。年老体弱者宜着厚棉衣、冬大衣或厚羽绒服。", 
       "index_uv": "中等", 
       "index48_uv": "弱", 
       "index_xc": "适宜", 
       "index_tr": "适宜", 
       "index_co": "舒适", 
       "st1": "17", 
       "st2": "5", 
       "st3": "17", 
       "st4": "5", 
       "st5": "18", 
       "st6": "6", 
       "index_cl": "适宜", 
       "index_ls": "适宜", 
       "index_ag": "极不易发"
   }
}
                */
						#endregion
					}
					catch 
					{

						throw (new ArgumentNullException());
					}
                }
            }
            else
            {
                result = "没有获取到该城市的天气,请确定输入了正确的城市名称,如\'北京\'或者\'beijing\'或者\'bj\'";
            }
            //返回
            return result;
        }
        /// <summary>
        /// 内部类：城市
        /// </summary>
        internal class City
        {
            /// <summary>
            /// 编码
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 拼音
            /// </summary>
            public string PinYin { get; set; }
            /// <summary>
            /// 拼音首字母
            /// </summary>
            public string FristLetter { get; set; }
        }
    }
}
