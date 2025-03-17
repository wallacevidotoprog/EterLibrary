using EterLibrary.Domain.Entities.DbModels;
using EterLibrary.Infrastructure;

namespace EterLibrary.Application.Services
{
	public static class DatabaseInitializer
	{
		public static void Initialize(DatabaseContext context)
		{
			context.Database.EnsureCreated();



			if (!context.Position.Any())
			{
				context.Position.AddRange(new List<PositionDbModel>
			{
				new PositionDbModel { ID = 130, NOME = "PADRÃO", PERMISSION = 0 },
				new PositionDbModel { ID = 131, NOME = "DEV", PERMISSION = 1 },
				new PositionDbModel { ID = 132, NOME = "ADMIN", PERMISSION = 2 },
				new PositionDbModel { ID = 133, NOME = "GERENTE", PERMISSION = 8 },
				new PositionDbModel { ID = 134, NOME = "FARMACÊUTICO(A)", PERMISSION = 16 },
				new PositionDbModel { ID = 135, NOME = "BALCONISTA", PERMISSION = 32 },
				new PositionDbModel { ID = 136, NOME = "OPERADOR DE LOJA", PERMISSION = 64 },
				new PositionDbModel { ID = 137, NOME = "OPERADOR DE CAIXA", PERMISSION = 128 },
				new PositionDbModel { ID = 138, NOME = "ENTREGADOR", PERMISSION = 256 },
				new PositionDbModel { ID = 150, NOME = "DEV", PERMISSION = 2 }
			});

				context.SaveChanges();
			}

			if (!context.Categoria.Any())
			{
				context.Categoria.AddRange(new List<CategoryDbModal> { new CategoryDbModal { ID = 1, NAME = "SEM CATEGORIA" } });

				context.SaveChanges();
			}

			if (!context.UserPossition.Any())
			{
				context.UserPossition.AddRange(new List<UserDbModel> { new UserDbModel { ID_LOJA = 0, NOME = "DEVELOPER", PASS = PasswordHelper.HasPassword("@321"), ID_FUNCAO = 150 } });

				context.SaveChanges();
			}

			if (!context.Payment.Any())
			{
				context.Payment.AddRange(new List<PaymentDbModal>
				{
					new PaymentDbModal { NAME = "CARTÃO" },
						new PaymentDbModal { NAME = "PIX" },
							new PaymentDbModal { NAME = "DINHEIRO" },
								new PaymentDbModal { NAME = "CONVENIO" },
									new PaymentDbModal { NAME = "OUTROS" },
				});
				context.SaveChanges();
			}

			if (!context.Situation.Any())
			{
				context.Situation.AddRange(new List<SituationDbModal>
				{
					new SituationDbModal { NAME = "A RECEBER" },
						new SituationDbModal { NAME = "PAGO" },
							new SituationDbModal { NAME = "NÃO INFORMADO" },
				});
				context.SaveChanges();
			}

			if (!context.DeliveryMethod.Any())
			{
				context.DeliveryMethod.AddRange(new List<DeliveryMethodDbModal>
				{
					new DeliveryMethodDbModal {     NAME = "A ENTREGAR NA RESIDÊNCIA DO CLIENTE" },
						new DeliveryMethodDbModal {     NAME = "O CLIENTE VEM RETIRAR" },
							new DeliveryMethodDbModal { NAME = "NÃO INFORMADO" },
				});
				context.SaveChanges();
			}
		}
	}
}
