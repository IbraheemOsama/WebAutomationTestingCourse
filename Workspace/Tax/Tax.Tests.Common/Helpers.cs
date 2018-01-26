using System.Collections.Generic;
using System.Linq;

namespace Tax.Tests.Common
{
    public static class Helpers
    {
        public static Dictionary<string, string> ToDictionary(this object data)
        {
            return data.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(data)?.ToString() ?? "");
        }
    }
}
