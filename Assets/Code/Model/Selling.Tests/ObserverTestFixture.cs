using System;
using NSubstitute;

namespace Assets.Code.Model.Selling.Tests
{
	public abstract class ObserverTestFixture<TEvent>
	{
		private IObserver<TEvent> _observer;

		public virtual void Setup()
		{
			_observer = Substitute.For<IObserver<TEvent>>();
			Observable.Subscribe(_observer);
		}

		protected abstract IObservable<TEvent> Observable { get; }

		protected void Assert_EventObserved(TEvent expected)
			=> _observer.Received().OnNext(expected);

		protected void Assert_EventObserved(TEvent expected, int repetitions)
			=> _observer.Received(repetitions).OnNext(expected);

		protected void Assert_EventNotObserved(TEvent prohibited)
			=> _observer.DidNotReceive().OnNext(prohibited);

		protected void Assert_EventsObserved(params TEvent[] expected)
			=> Received.InOrder(() =>
			{
				foreach (var expectedEvent in expected)
					_observer.Received().OnNext(expectedEvent);
			});
	}
}