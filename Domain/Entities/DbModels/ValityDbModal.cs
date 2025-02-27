namespace EterLibrary.Domain.Entities.DbModels
{
	public class ValityDbModal : BaseDbModal
	{
		public long? ID_USER { get; set; }
		public DateTime? DATE { get; set; }

		public UserDbModel? UserModel { get; set; }

		public ICollection<ProductValidadeDbModal> ProductValidades = new List<ProductValidadeDbModal>();
	}
}
