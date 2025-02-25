namespace EterLibrary.Domain.Entities.DbModels
{
	public class SituationDbModal : BaseDbModal
	{
		public string? NAME { get; set; }
	
		public ICollection<ManipulationDbModel>? Manipulation =new List<ManipulationDbModel>();
	}
}
