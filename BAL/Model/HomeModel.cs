using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Model
{
    public class HomeModel
    {
        public string Name { get; set; }
        public Int64 Count { get; set; }
    }

    public class ChartModel
    {
        public List<string> JsonLabel = new List<string>();
        public List<string> JsonData = new List<string>();
        public List<HomeModel> objDashboardMonth = new List<HomeModel>();
        public List<HomeModel> objDashboardYear = new List<HomeModel>();

        public Int64 MaxValue { get; set; }
    }
}
