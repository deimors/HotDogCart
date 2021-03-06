﻿using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.CustomersTests
{
	public abstract class CustomersTestFixture : ObserverTestFixture<CustomersEvent>
	{
		private Customers _customers;
		protected abstract int MaxLineLength { get; }

		[SetUp]
		public override void Setup()
		{
			_customers = new Customers(MaxLineLength);

			base.Setup();
		}

		protected override IObservable<CustomersEvent> Observable => _customers.Events;

		protected void Arrange_SaleStarted()
			=> _customers.CartObserver.OnNext(new SaleStartedEvent());

		protected void Act_AddWaitingCustomer()
			=> _customers.AddWaitingCustomer();
	}
}