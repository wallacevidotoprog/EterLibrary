namespace EterLibrary.Domain.Entities.DbModels
{
	public class ReqNotaDbModal : BaseDbModal
	{
		public long? CQN_ID { get; set; }
		public string? REQ { get; set; }

		public ControlReqNotaDbModal? ControlReqNota { get; set; }
	}
}
