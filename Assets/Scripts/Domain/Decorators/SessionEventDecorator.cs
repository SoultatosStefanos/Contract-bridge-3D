using System;
using System.Collections.Generic;
using ContractBridge.Core;
using Domain.Events;
using Events;
using Zenject;

namespace Domain.Decorators
{
    public class SessionEventDecorator : ISession
    {
        private readonly ISession _session;

        [Inject]
        private IEventBus _eventBus;

        public SessionEventDecorator(ISession session)
        {
            _session = session;

            _session.PhaseChanged += OnPhaseChanged;
        }

        public IPair Pair(Seat seat)
        {
            return _session.Pair(seat);
        }

        public IPair OtherPair(Seat seat)
        {
            return _session.OtherPair(seat);
        }

        public IPair OtherPair(IPair pair)
        {
            return _session.OtherPair(pair);
        }

        public Phase Phase => _session.Phase;

        public IDeck Deck => _session.Deck;

        public IBoard Board => _session.Board;

        public IAuction Auction => _session.Auction;

        public IGame Game => _session.Game;

        public IEnumerable<IPair> Pairs => _session.Pairs;

        public event EventHandler<ISession.PhaseEventArgs> PhaseChanged
        {
            add => _session.PhaseChanged += value;
            remove => _session.PhaseChanged -= value;
        }

        private void OnPhaseChanged(object sender, ISession.PhaseEventArgs e)
        {
            _eventBus.Post(new SessionPhaseChangedEvent(this, e.Phase));
        }
    }
}