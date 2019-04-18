using System;
using Assets.Code.Integration;
using Assets.Code.Model.Selling;
using Zenject;

namespace Assets.Code.Infrastructure
{
	public class SceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<Customers>().AsSingle().WithArguments(2).NonLazy();
			Container.Bind<HotDogCart>().AsSingle().WithArguments(TimeSpan.FromMinutes(1)).NonLazy();
			Container.Bind<Grill>().AsSingle().NonLazy();
			Container.Bind<Time>().AsSingle().NonLazy();

			Container.Bind<TimePump>().AsSingle().NonLazy();
			Container.Bind<HotDogCartCustomerGenerator>().AsSingle().NonLazy();

			Container.Bind<IInitializable>().To<CustomersBinding>().AsSingle();
			Container.Bind<IInitializable>().To<HotDogCartBindings>().AsSingle();
			Container.Bind<IInitializable>().To<GrillBindings>().AsSingle();
		}
	}
}
