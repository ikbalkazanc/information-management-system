using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Databases
{
    public class NoticeDB
    {
        public int user_id { get; set; }
        public int notice_id { get; set; }

        public DateTime notice_date = new DateTime(1, 1, 1);
        public string notice_workfield { get; set; }
        public string notice_title { get; set; }
        public string notice_text { get; set; }
        public bool isIntern { get; set; }

        public GraduateDB notice_graduate = new GraduateDB();
    }
}
