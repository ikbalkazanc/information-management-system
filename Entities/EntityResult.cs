using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntityResult<T> where T : class
    {
        public bool Result { get; set; }
        public string ErrorText { get; set; }
        public T Object { get; set; }
        public IList<T> Objects { get; set; }


    }
}
