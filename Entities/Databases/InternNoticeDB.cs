using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Databases
{
    public class InternNoticeDB : NoticeDB
    {
        public string notice_intern_period { get; set; } 
        //stajyer hangi dönem çalışıcak

        public int notice_intern_lenght_days { get; set; }  
        //stajyer kaç gün çalışıcak
    }
}
