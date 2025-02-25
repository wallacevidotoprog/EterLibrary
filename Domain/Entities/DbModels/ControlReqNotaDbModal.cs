namespace EterLibrary.Domain.Entities.DbModels
{
	public class ControlReqNotaDbModal : BaseDbModal
	{
		public long? VENDEDOR { get; set; }

		public long? AUTHOR { get; set; }

		public DateTime? DATA_VENDA { get; set; }

		public DateTime? DATA_ENVIO { get; set; }

		public UserDbModel? UserModel_v { get; set; }
		public UserDbModel? UserModel_a { get; set; }

		public ICollection<ReqNotaDbModal>? ReqNota { get; set; }
	}
}
