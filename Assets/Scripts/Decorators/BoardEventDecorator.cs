using System;
using System.Collections.Generic;
using ContractBridge.Core;
using Events;
using Zenject;

namespace Decorators
{
    public class BoardEventDecorator : IBoard
    {
        private readonly IBoard _board;

        [Inject]
        private IEventBus _eventBus;

        public BoardEventDecorator(IBoard board)
        {
            _board = board;

            _board.DealerSet += OnDealerSet;
            _board.VulnerabilitySet += OnVulnerabilitySet;
        }

        public string ToPbn()
        {
            return _board.ToPbn();
        }

        public IHand Hand(Seat seat)
        {
            return _board.Hand(seat);
        }

        public IEnumerable<IHand> OtherHands(Seat seat)
        {
            return _board.OtherHands(seat);
        }

        public IEnumerable<IHand> OtherHands(IHand hand)
        {
            return _board.OtherHands(hand);
        }

        public Seat? Dealer
        {
            get => _board.Dealer;
            set => _board.Dealer = value;
        }

        public Vulnerability? Vulnerability
        {
            get => _board.Vulnerability;
            set => _board.Vulnerability = value;
        }

        public IEnumerable<IHand> Hands => _board.Hands;

        public event EventHandler<IBoard.DealerEventArgs> DealerSet
        {
            add => _board.DealerSet += value;
            remove => _board.DealerSet -= value;
        }

        public event EventHandler<IBoard.VulnerabilityEventArgs> VulnerabilitySet
        {
            add => _board.VulnerabilitySet += value;
            remove => _board.VulnerabilitySet -= value;
        }

        private void OnDealerSet(object sender, IBoard.DealerEventArgs e)
        {
            _eventBus.Post(new BoardDealerSetEvent(this, e.Dealer));
        }

        private void OnVulnerabilitySet(object sender, IBoard.VulnerabilityEventArgs e)
        {
            _eventBus.Post(new BoardVulnerabilitySetEvent(this, e.Vulnerability));
        }
    }
}