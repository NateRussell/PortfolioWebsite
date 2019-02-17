using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite.Models
{
    public class Model : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
