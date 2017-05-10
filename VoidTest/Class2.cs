using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoidTest
{
    [ComClass(Class2.ClassId, Class2.InterfaceId, Class2.EventsId)]
    public class Class2
    {
        public const string ClassId = "c936ca83-5e0f-476c-b60e-4378f116f008";

        public const string InterfaceId = "b705070b-8027-4fd6-9e8b-1d65d615f571";

        public const string EventsId = "1e8bdf20-cfdb-402c-979f-30da4fb08111";

        
        internal Class2(Class1 parent)
        {
            _parent = parent;
        }
        private Class1 _parent;
        private DateTime _hostOrderDate;

        internal Class1 Parent
        {
            get
            {
                return _parent;
            }
        }

        public string HostOrderDate
        {
            get
            {
                return _hostOrderDate.ToShortDateString(); ;
            }
            set
            {

                _hostOrderDate = DateTime.Today;
            }
        }
    }
}

