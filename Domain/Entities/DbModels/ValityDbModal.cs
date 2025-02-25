namespace EterLibrary.Domain.Entities.DbModels
{
	public class ValityDbModal : BaseDbModal
	{
		public long? ID_USER { get; set; }
		public long? DATE { get; set; }

		public UserDbModel? UserModel { get; set; }
	}
}
