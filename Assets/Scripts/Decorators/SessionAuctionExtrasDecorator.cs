using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractBridge.Core;
using ContractBridge.Solver;
using Domain;

namespace Decorators
{
    public class SessionAuctionExtrasDecorator : ISession
    {
        private const int AnalyzeBoardDelay = 3000; // More intuitive for the user :)

        private readonly IAuctionExtras _auctionExtras;

        private readonly IDoubleDummySolver _doubleDummySolver;

        private readonly ISession _session;

        public SessionAuctionExtrasDecorator(
            ISession session,
            IAuctionExtras auctionExtras,
            IDoubleDummySolver doubleDummySolver
        )
        {
            _session = session;
            _auctionExtras = auctionExtras;
            _doubleDummySolver = doubleDummySolver;

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

        private async void OnPhaseChanged(object sender, ISession.PhaseEventArgs e)
        {
            if (e.Phase == Phase.Auction)
            {
                await AnalyzeBoard();
            }
        }

        private async Task AnalyzeBoard()
        {
            await Task.Delay(AnalyzeBoardDelay);

            var solution = await Task.Run(() => _doubleDummySolver.Analyze(this));

            _auctionExtras.Solution = solution;
        }
    }
}