using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Models
{
    public enum DBOperationType
    {
        SELECT = 1,
        INSERT = 2,
        DELETE = 4,
        UPDATE = 8,        
    }
}
