using JZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Cache
{
    public class CacheHelper
    {    
        [Cacher("Sys_UserInfo", typeof(Sys_UserInfo))]
        public static List<Sys_UserInfo> LstFoodCache { get; set; }
    }
}
