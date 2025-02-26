namespace EterLibrary.Domain.Entities.DbModels
{
	public class UserDbModel : BaseDbModal
	{
		public long? ID_LOJA { get; set; }

		public string? NOME { get; set; }

		public string? PASS { get; set; }

		public long? ID_FUNCAO { get; set; }

		public bool STATUS { get; set; } = true;

		public PositionDbModel? Position { get; set; }

		public ICollection<ValityDbModal> Vality { get; set; } = new List<ValityDbModal>();

		public ICollection<CategoryDbModal> Category { get; set; } = new List<CategoryDbModal>();

		public ICollection<ControlReqNotaDbModal> ControlReqNota_v { get; set; } = new List<ControlReqNotaDbModal>();

		public ICollection<ControlReqNotaDbModal> ControlReqNota_a { get; set; } = new List<ControlReqNotaDbModal>();

		public ICollection<ManipulationDbModel> Manipulation { get; set; } = new List<ManipulationDbModel>();

		public ICollection<MedControlDbModel> MedControl { get; set; } = new List<MedControlDbModel>();


		

	}
}
