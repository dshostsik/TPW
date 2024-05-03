using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationModel
{
    public abstract class iModelAPI
    {
        public static iModelAPI Instance() { 
            return new ModelAPI();
        }



    }
}
