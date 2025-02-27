namespace EterLibrary.Domain.Entities.DbModels
{
	public class ProductValidadeDbModal : BaseDbModal
	{
		public long? ID_VALIDADE { get; set; }

		public int? PRODUTO_CODIGO { get; set; }

		public string? PRODUTO_DESCRICAO { get; set; }

		public int? QUANTIDADE { get; set; }

		public long? ID_CATEGORIA { get; set; }

		public DateTime? DATA_VALIDADE { get; set; }

		public ValityDbModal? Vality { get; set; }

		public CategoryDbModal? Category { get; set; }

	}
}
