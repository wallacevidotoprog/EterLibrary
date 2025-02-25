using EterLibrary.Domain.Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace EterLibrary.Infrastructure
{
	public class DatabaseContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=meuBanco.db");
		}

		public DbSet<CategoryDbModal> Categoria { get; set; }
		public DbSet<AddressClienteDbModel> Address { get; set; }
		public DbSet<ClientDbModel> Client { get; set; }
		public DbSet<ControlReqNotaDbModal> ControlReqNota { get; set; }
		public DbSet<DeliveryMethodDbModal> DeliveryMethod { get; set; }
		public DbSet<PositionDbModel> Position { get; set; }
		public DbSet<ManipulationDbModel> Manipulation { get; set; }
		public DbSet<MedControlDbModel> MedControl { get; set; }
		public DbSet<MedManipulationDbModal> MedManipulation { get; set; }
		public DbSet<PaymentDbModal> Payment { get; set; }
		public DbSet<ProductValidadeDbModal> ProductValidade { get; set; }
		public DbSet<ReqNotaDbModal> ReqNota { get; set; }
		public DbSet<SituationDbModal> Situation { get; set; }
		public DbSet<UserDbModel> UserPossition { get; set; }
		public DbSet<ValityDbModal> Vality { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ValityDbModal>().ToTable("Vality");
			modelBuilder.Entity<UserDbModel>().ToTable("UserPossition");
			modelBuilder.Entity<CategoryDbModal>().ToTable("Category");
			modelBuilder.Entity<AddressClienteDbModel>().ToTable("Address");
			modelBuilder.Entity<ClientDbModel>().ToTable("Client");
			modelBuilder.Entity<ControlReqNotaDbModal>().ToTable("ControlReqNota");
			modelBuilder.Entity<DeliveryMethodDbModal>().ToTable("DeliveryMethod");
			modelBuilder.Entity<PositionDbModel>().ToTable("Position");
			modelBuilder.Entity<ManipulationDbModel>().ToTable("Manipulation");
			modelBuilder.Entity<MedControlDbModel>().ToTable("MedControl");
			modelBuilder.Entity<MedManipulationDbModal>().ToTable("MedManipulation");
			modelBuilder.Entity<PaymentDbModal>().ToTable("Payment");
			modelBuilder.Entity<ProductValidadeDbModal>().ToTable("ProductValidade");
			modelBuilder.Entity<ReqNotaDbModal>().ToTable("ReqNota");
			modelBuilder.Entity<SituationDbModal>().ToTable("Situation");

			modelBuilder.Entity<ValityDbModal>()
				.HasOne(x => x.UserModel)
				.WithMany(x => x.Vality)
				.HasForeignKey(x => x.ID_USER);

			modelBuilder.Entity<CategoryDbModal>()
				.HasOne(x => x.UserModel)
				.WithMany(x => x.Category)
				.HasForeignKey(x => x.ID_USER);

			modelBuilder.Entity<ControlReqNotaDbModal>()
				.HasOne(c => c.UserModel_v)
				.WithMany(x => x.ControlReqNota_v)
				.HasForeignKey(c => c.VENDEDOR);

			modelBuilder.Entity<ControlReqNotaDbModal>()
				.HasOne(c => c.UserModel_a)
				.WithMany(x => x.ControlReqNota_a)
				.HasForeignKey(c => c.AUTHOR)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedControlDbModel>()
				.HasOne(x => x.UserModel)
				.WithMany(x => x.MedControl)
				.HasForeignKey(x => x.ID_USER);

			modelBuilder.Entity<MedControlDbModel>()
				.HasOne(x => x.ClientDbModel)
				.WithMany(x => x.MedControl)
				.HasForeignKey(x => x.ID_CLIENT);

			modelBuilder.Entity<AddressClienteDbModel>()
				.HasOne(x => x.Client)
				.WithMany(x => x.AddressCliente)
				.HasForeignKey(x => x.ID_CLIENT);

			modelBuilder.Entity<ManipulationDbModel>()
				.HasOne(x => x.Client)
				.WithMany(x => x.Manipulation)
				.HasForeignKey(x => x.ID_CLIENTE);

			modelBuilder.Entity<ManipulationDbModel>()
				.HasOne(x => x.AddressCliente)
				.WithMany(x => x.Manipulation)
				.HasForeignKey(x => x.ID_ENDERECO);

			modelBuilder.Entity<ManipulationDbModel>()
				.HasOne(x => x.UserModel)
				.WithMany(x => x.Manipulation)
				.HasForeignKey(x => x.ATEN_LOJA);

			modelBuilder.Entity<UserDbModel>()
				.HasOne(x => x.Position)
				.WithMany(x => x.UserModel)
				.HasForeignKey(x => x.ID_FUNCAO);

			modelBuilder.Entity<ProductValidadeDbModal>()
				.HasOne(x => x.Category)
				.WithMany(x => x.ProductValidade)
				.HasForeignKey(x => x.ID_CATEGORIA);

			modelBuilder.Entity<ReqNotaDbModal>()
				.HasOne(x => x.ControlReqNota)
				.WithMany(x => x.ReqNota)
				.HasForeignKey(x => x.CQN_ID);

			modelBuilder.Entity<MedControlDbModel>()
			.HasOne(x => x.AddressCliente)
			.WithMany(x => x.MedControl)
			.HasForeignKey(x => x.ID_ADDRESS);

			modelBuilder.Entity<ManipulationDbModel>()
			.HasOne(x => x.Situation)
			.WithMany(x => x.Manipulation)
			.HasForeignKey(x => x.ID_SITUCAO);

			modelBuilder.Entity<ManipulationDbModel>()
			.HasOne(x => x.Payment)
			.WithMany(x => x.Manipulation)
			.HasForeignKey(x => x.ID_FORMAPAGAMENTO);

			modelBuilder.Entity<ManipulationDbModel>()
			.HasOne(x => x.DeliveryMethod)
			.WithMany(x => x.Manipulation)
			.HasForeignKey(x => x.ID_MODOENTREGA);



		}
	}

}

