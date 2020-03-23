using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _contex;

        public SeedDB(DataContext contex)
        {
            _contex = contex;
        }
    }
}
