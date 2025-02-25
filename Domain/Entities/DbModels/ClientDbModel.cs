namespace EterLibrary.Domain.Entities.DbModels
{
	public class ClientDbModel : BaseDbModal
	{
		public string? CPF { get; set; }

		public string? RG { get; set; }

		public string? NOME { get; set; }

		public string? PHONE { get; set; }

		public ICollection<MedControlDbModel> MedControl { get; set; } = new List<MedControlDbModel>();

		public ICollection<AddressClienteDbModel> AddressCliente { get; set; } = new List<AddressClienteDbModel>();

		public ICollection<ManipulationDbModel> Manipulation { get; set; } = new List<ManipulationDbModel>();
	}
}
