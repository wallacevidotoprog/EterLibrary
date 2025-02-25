namespace EterLibrary.Domain.Entities.DbModels
{
	public class MedControlDbModel : BaseDbModal
	{
		public long? ID_CLIENT { get; set; }

		public long? ID_ADDRESS { get; set; }

		public long? ID_USER { get; set; }

		public int? CODIGO { get; set; }

		public string? NAME_M { get; set; }

		public int QTD { get; set; }

		public DateTime? VALIDADE { get; set; }

		public string? LOTE { get; set; }

		public UserDbModel? UserModel { get; set; }

		public ClientDbModel? ClientDbModel { get; set; }

		public AddressClienteDbModel? AddressCliente { get; set; }
	}
}
