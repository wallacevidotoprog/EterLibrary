namespace EterLibrary.Domain.Entities.DbModels
{
	public abstract class BaseDbModal
	{
		public long? ID { get; set; }

		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime? CREATE_AT { get; set; }

		public DateTime? UPDATE_AT { get; set; }

	}
}
