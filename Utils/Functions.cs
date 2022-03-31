using System;

namespace Helpers
{
    public class Functions
    {
        public static string UUID()
        {
            Guid guid = Guid.NewGuid();
            string uuid = guid.ToString();
            return uuid;
        }
    }
}
