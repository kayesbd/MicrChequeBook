using System.Linq;


namespace KBZ.Utils
{
    public static class ExtensionMethods
    {
        public static string ToMaskedAccountNo(this string accountNo)
        {
            var accountArr = accountNo.ToCharArray();

            for (int i = 0; i < accountArr.Length - 4; i++)
            {
                accountArr[i] = '*';
            }

            return new string(accountArr);
        }

        public static bool IsNumber(this string name)
        {
            char[] chars = name.ToCharArray();

            return chars.All(char.IsNumber);
        }
        
        
    }
}
