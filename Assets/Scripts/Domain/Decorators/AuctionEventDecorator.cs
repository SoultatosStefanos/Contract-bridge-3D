using System;
using System.Collections.Generic;
using ContractBridge.Core;
using Domain.Events;
using Events;
using Zenject;

namespace Domain.Decorators
{
    public class AuctionEventDecorator : IAuction
    {
        private readonly IAuction _auction;

        [Inject]
        private IEventBus _eventBus;

        public AuctionEventDecorator(IAuction auction)
        {
            _auction = auction;

            _auction.TurnChanged += OnTurnChanged;
            _auction.Called += OnCalled;
            _auction.Passed += OnPassed;
            _auction.Doubled += OnDoubled;
            _auction.Redoubled += OnRedoubled;
            _auction.FinalContractMade += OnFinalContractMade;
        }

        public bool CanCall(IBid bid, Seat seat)
        {
            return _auction.CanCall(bid, seat);
        }

        public bool CanPass(Seat seat)
        {
            return _auction.CanPass(seat);
        }

        public bool CanDouble(Seat seat)
        {
            return _auction.CanDouble(seat);
        }

        public void Call(IBid bid, Seat seat)
        {
            _auction.Call(bid, seat);
        }

        public void Pass(Seat seat)
        {
            _auction.Pass(seat);
        }

        public void Double(Seat seat)
        {
            _auction.Double(seat);
        }

        public IContract FinalContract => _auction.FinalContract;

        public IEnumerable<IBid> AllBids => _auction.AllBids;

        public Seat? FirstTurn
        {
            get => _auction.FirstTurn;
            set => _auction.FirstTurn = value;
        }

        public Seat? Turn => _auction.Turn;

        public event EventHandler<IAuction.TurnEventArgs> TurnChanged
        {
            add => _auction.TurnChanged += value;
            remove => _auction.TurnChanged -= value;
        }

        public event EventHandler<IAuction.CallEventArgs> Called
        {
            add => _auction.Called += value;
            remove => _auction.Called -= value;
        }

        public event EventHandler<IAuction.PassEventArgs> Passed
        {
            add => _auction.Passed += value;
            remove => _auction.Passed -= value;
        }

        public event EventHandler<IAuction.DoubleEventArgs> Doubled
        {
            add => _auction.Doubled += value;
            remove => _auction.Doubled -= value;
        }

        public event EventHandler<IAuction.RedoubleEventArgs> Redoubled
        {
            add => _auction.Redoubled += value;
            remove => _auction.Redoubled -= value;
        }

        public event EventHandler<IAuction.ContractEventArgs> FinalContractMade
        {
            add => _auction.FinalContractMade += value;
            remove => _auction.FinalContractMade -= value;
        }

        public event EventHandler PassedOut
        {
            add => _auction.PassedOut += value;
            remove => _auction.PassedOut -= value;
        }

        private void OnTurnChanged(object sender, IAuction.TurnEventArgs e)
        {
            _eventBus.Post(new AuctionTurnChangeEvent(this, e.Seat));
        }

        private void OnCalled(object sender, IAuction.CallEventArgs e)
        {
            _eventBus.Post(new AuctionCallEvent(this, e.Bid, e.Seat));
        }

        private void OnPassed(object sender, IAuction.PassEventArgs e)
        {
            _eventBus.Post(new AuctionPassEvent(this, e.Seat));
        }

        private void OnDoubled(object sender, IAuction.DoubleEventArgs e)
        {
            _eventBus.Post(new AuctionDoubleEvent(this, e.Seat));
        }

        private void OnRedoubled(object sender, IAuction.RedoubleEventArgs e)
        {
            _eventBus.Post(new AuctionRedoubleEvent(this, e.Seat));
        }

        private void OnFinalContractMade(object sender, IAuction.ContractEventArgs e)
        {
            _eventBus.Post(new AuctionFinalContractEvent(this, e.Contract));
        }
    }
}