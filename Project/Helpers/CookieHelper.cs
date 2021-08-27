using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Helpers
{
    public static class CookieHelper
    {
        static int _cookieExpiration = 10;
        public static void SetProductListId(this IResponseCookies cookie, string key, List<int> value)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Count; i++)
            {
                if(i == value.Count - 1) sb.Append(value[i].ToString());
                else sb.Append(value[i].ToString() + ",");
            }
            
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(_cookieExpiration);
            cookie.Append(key, sb.ToString(), option);
        }

        public static List<int> GetProductListId(this IRequestCookieCollection cookie, string key)
        {
            string data = cookie[key];
            if (string.IsNullOrEmpty(data) || data == "[]") return new List<int>();
            List<string> list = data.Split(",").ToList();
            List<int> idList = new List<int>();
            foreach (var item in list)
            {
                idList.Add(int.Parse(item));
            }
            return idList;       
        }
    }
}
