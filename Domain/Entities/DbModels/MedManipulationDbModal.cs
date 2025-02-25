namespace EterLibrary.Domain.Entities.DbModels
{
	public class MedManipulationDbModal : BaseDbModal
	{
		public long? ID_MANIPULADOS { get; set; }
		public string? NAME_M { get; set; }

		public ManipulationDbModel? Manipulation { get; set; }
	}
}
