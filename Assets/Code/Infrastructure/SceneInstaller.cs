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
			Container.Bind<Customers>().AsSingle().NonLazy();
			
			Container.Bind<HotDogCart>().AsSingle().WithArguments(TimeSpan.FromMinutes(1)).NonLazy();

			Container.Bind<IObservable<CustomersEvent>>()
				.FromMethod(context => context.Container.Resolve<Customers>().Events)
				.AsSingle();

			Container.Bind<HotDogCartTimePump>().AsSingle().NonLazy();

			Container.Bind<HotDogCartCustomerGenerator>().AsSingle().NonLazy();
		}
	}
}
