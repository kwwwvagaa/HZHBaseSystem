using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Tools
{
    public class ExcuteResultMsg
    {
        public ResultCode Code
        {
            get;
            set;
        }

        public string ErrorMsg
        {
            get;
            set;
        }

        public string Result
        {
            get;
            set;
        }

        public object objEntity
        {
            get;
            set;
        }
    }
}
