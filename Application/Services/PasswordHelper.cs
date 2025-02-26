namespace EterLibrary.Application.Services
{
	public class PasswordHelper
	{
		public static string HasPassword(string pass)
		{
			return BCrypt.Net.BCrypt.HashPassword(pass);
		}

		public static bool VerifyPassword(string pass, string hash)
		{
			return BCrypt.Net.BCrypt.Verify(pass, hash);
		}
	}
}
