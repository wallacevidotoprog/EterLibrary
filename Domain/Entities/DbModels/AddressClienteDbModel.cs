using System.ComponentModel.DataAnnotations.Schema;

namespace EterLibrary.Domain.Entities.DbModels
{
	public class AddressClienteDbModel : BaseDbModal
	{
		[ForeignKey("ID_CLIENT")]
		public long? ID_CLIENT { get; set; }

		public string? PLACE { get; set; }

		public int? NUMBER { get; set; }

		public string? ZONE { get; set; }

		public string? CITY { get; set; }

		public string? UF { get; set; }

		public string? OBSERVACAO { get; set; }

		public ClientDbModel? Client { get; set; }

		public ICollection<ManipulationDbModel> Manipulation { get; set; } = new List<ManipulationDbModel>();

		public ICollection<MedControlDbModel> MedControl { get; set; } = new List<MedControlDbModel>();

		public override string ToString()
		{
			return
				(!string.IsNullOrEmpty(PLACE) ? PLACE : string.Empty) +
				(NUMBER != null ? ", " + NUMBER.ToString() : string.Empty) +
				(!string.IsNullOrEmpty(ZONE) ? ", " + ZONE : string.Empty) +
				(!string.IsNullOrEmpty(CITY) ? ", " + CITY : string.Empty) +
				(!string.IsNullOrEmpty(UF) ? " - " + UF : string.Empty);
		}
		public string ToStringDrop()
		{
			return
				(!string.IsNullOrEmpty(PLACE) ? PLACE : string.Empty) +
				(NUMBER != null ? ", " + NUMBER.ToString() : string.Empty) +
				(!string.IsNullOrEmpty(ZONE) ? ", " + ZONE : string.Empty) +
				(!string.IsNullOrEmpty(CITY) ? ", " + CITY : string.Empty) +
				(!string.IsNullOrEmpty(UF) ? " - " + UF : string.Empty);
		}

	}
}
