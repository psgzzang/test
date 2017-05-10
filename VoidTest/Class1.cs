using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM;
using SPM.Configuration;
using Microsoft.VisualBasic;

namespace VoidTest
{

    [ComClass(Class1.ClassId, Class1.InterfaceId, Class1.EventsId)]
    public class Class1 : IDisposable
    {
        public const string ClassId = "00010349-fb71-42e9-9755-4a4c5ec0328c";

        public const string InterfaceId = "79270f02-3745-4d79-a164-82d1b82fe6b9";

        public const string EventsId = "26dae29f-2fe7-444e-acda-cbe385c24215";

        Class2 _order;
      
        public Class2 Order
        {
            get
            {
                if ((_order == null))
                {
                    _order = new Class2(this);

                }

                return _order;
            }
        }

        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            GC.SuppressFinalize(this);
        }
   
    }
}
