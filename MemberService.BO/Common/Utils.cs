

namespace MemberService.BO.Common
{
    public class Utils
    {
        public static string GenerateCode(string prefix)
        {
            // Generate a random integer between 0 and 99999 and format as 5 digits
            var number = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 100000);
            return $"{prefix}_{number:D5}";
        }
    }
}