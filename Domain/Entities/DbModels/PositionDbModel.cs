namespace EterLibrary.Domain.Entities.DbModels
{
	public class PositionDbModel : BaseDbModal
	{
		public string? NOME { get; set; }
		public int? PERMISSION { get; set; }

		public ICollection<UserDbModel>? UserModel = new List<UserDbModel>();
	}
}
