namespace EterLibrary.Domain.Entities.DbModels
{
	public class PaymentDbModal : BaseDbModal
	{
		public string? NAME { get; set; }

		public ICollection<ManipulationDbModel>? Manipulation = new List<ManipulationDbModel>();
	}
}
