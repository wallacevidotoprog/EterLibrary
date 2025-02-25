
namespace EterLibrary.Domain.Entities.DbModels
{
	public class ManipulationDbModel : BaseDbModal
	{
		public long? ATEN_LOJA { get; set; }

		public DateTime? DATA { get; set; }

		public string? ATEN_MANI { get; set; }

		public long? ID_CLIENTE { get; set; }

		public long? ID_ENDERECO { get; set; }

		public long? ID_SITUCAO { get; set; }

		public long? ID_FORMAPAGAMENTO { get; set; }

		public long? ID_MODOENTREGA { get; set; }

		public decimal? VALORFINAL { get; set; }

		public string? OBSGERAL { get; set; }


		public UserDbModel? UserModel { get; set; }

		public ClientDbModel? Client { get; set; }

		public AddressClienteDbModel? AddressCliente { get; set; }

		public ICollection<MedManipulationDbModal>? MedManipulation = new List<MedManipulationDbModal>();

		public PaymentDbModal? Payment { get; set; }
		public SituationDbModal? Situation { get; set; }
		public DeliveryMethodDbModal? DeliveryMethod { get; set; }


	}


}
