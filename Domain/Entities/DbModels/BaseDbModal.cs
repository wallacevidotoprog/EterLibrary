namespace EterLibrary.Domain.Entities.DbModels
{
	public abstract class BaseDbModal
	{
		public long? ID { get; set; }

		public DateTime? CREATE_AT { get; set; }

		public DateTime? UPDATE_AT { get; set; }

	}
}
