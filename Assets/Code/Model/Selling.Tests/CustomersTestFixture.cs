﻿using System;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public abstract class CustomersTestFixture : ObserverTestFixture<CustomersEvent>
	{
		private Customers _customers;
		protected abstract int LineLength { get; }

		[SetUp]
		public override void Setup()
		{
			_customers = new Customers(LineLength);

			base.Setup();
		}

		protected override IObservable<CustomersEvent> Observable => _customers.Events;

		protected void Arrange_SaleStarted()
			=> _customers.CartObserver.OnNext(new SaleStartedEvent());

		protected void Act_AddWaitingCustomer()
			=> _customers.AddWaitingCustomer();
	}
}