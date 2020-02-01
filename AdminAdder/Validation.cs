using System;

namespace AdminAdder
{
    class Validation
    {
        public static bool IfOnlyNumber(string userS)
        {
            if (userS == null || userS == "") return false;
            foreach (char item in userS)
            {
                if (!Char.IsDigit(item)) return false;
            }
            return true;
        }
    }
}
