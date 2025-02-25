namespace EterLibrary.Domain.Entities.DbModels
{
	public class CategoryDbModal : BaseDbModal
	{
		public string? NAME { get; set; }
		public long? ID_USER { get; set; }

		public UserDbModel? UserModel { get; set; }

		public ICollection<ProductValidadeDbModal>? ProductValidade { get; set; }
	}
}
