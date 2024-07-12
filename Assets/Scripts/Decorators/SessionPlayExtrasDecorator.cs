using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ContractBridge.Core;
using ContractBridge.Solver;
using Domain;
using Events;

namespace Decorators
{
    public class SessionPlayExtrasDecorator : ISession
    {
        private readonly IDoubleDummySolver _doubleDummySolver;

        private readonly IPlayExtras _playExtras;
        private readonly ISession _session;

        public SessionPlayExtrasDecorator(
            ISession session,
            IPlayExtras playExtras,
            IDoubleDummySolver doubleDummySolver,
            IEventBus eventBus
        )
        {
            _session = session;
            _playExtras = playExtras;
            _doubleDummySolver = doubleDummySolver;

            eventBus.On<GameTurnChangeEvent>(HandleGameTurnChangeEvent);
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

        public void Reset()
        {
            _session.Reset();
        }

        public event EventHandler<ISession.PhaseEventArgs> PhaseChanged
        {
            add => _session.PhaseChanged += value;
            remove => _session.PhaseChanged -= value;
        }

        private async void HandleGameTurnChangeEvent(GameTurnChangeEvent e)
        {
            await AnalyzeOptimalPlays();
        }

        private async Task AnalyzeOptimalPlays()
        {
            var solution = await Task.Run(() =>
            {
                Debug.Assert(_session.Auction != null, "_session.Auction != null");
                Debug.Assert(_session.Auction.FinalContract != null, "_session.Auction.FinalContract != null");

                return _doubleDummySolver.AnalyzePlays(_session, _session.Auction.FinalContract);
            });

            _playExtras.Solution = solution;
        }
    }
}