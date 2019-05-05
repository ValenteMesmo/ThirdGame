using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class ServerIpFinder
    {
        public string FindIp(IEnumerable<string> infos, string myIp)
        {
            var lowestip = infos.OrderBy(f => f).FirstOrDefault();
            if (lowestip == null)
                return myIp;

            else if (lowestip.CompareTo(myIp) < 0)
                return lowestip;

            return myIp;
        }
    }
}
