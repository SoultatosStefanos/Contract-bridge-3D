using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ContractBridge.Core;
using Domain;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace AI
{
    public class GameAI : MonoBehaviour
    {
        [FormerlySerializedAs("Delay")]
        [SerializeField]
        [Tooltip("The time to wait (in seconds) before invoking the AI action at each turn.")]
        private float delay = 3.0f;

        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private IPlayExtras _playExtras;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<PlayExtrasPlaysSolutionSetEvent>(HandlePlayExtrasPlaysSolutionSetEvent);

            if (_session.Game?.Turn is { } turn)
            {
                TakeTurnIfOnTurn(turn);
            }
        }

        private void OnDisable()
        {
            _eventBus.Off<PlayExtrasPlaysSolutionSetEvent>(HandlePlayExtrasPlaysSolutionSetEvent);
        }

        private void HandlePlayExtrasPlaysSolutionSetEvent(PlayExtrasPlaysSolutionSetEvent e)
        {
            if (Game().Turn is { } turn)
            {
                TakeTurnIfOnTurn(turn);
            }
        }

        private void TakeTurnIfOnTurn(Seat turn)
        {
            var dummySeat = DummySeat();

            if (turn == playerSeat && dummySeat != playerSeat)
            {
                return;
            }

            if (turn == dummySeat && dummySeat == playerSeat.Partner())
            {
                return;
            }

            StartCoroutine(TakeTurn(turn));
        }

        private IEnumerator TakeTurn(Seat aiSeat)
        {
            yield return new WaitForSeconds(delay);

            var hand = _session.Board.Hand(aiSeat);
            var cardToPlay = ChooseCardToPlay(hand, aiSeat);

            if (cardToPlay != null)
            {
                Game().Follow(cardToPlay, aiSeat);
            }
            // TODO(?) Game finished. probably nothing
        }

        private ICard ChooseCardToPlay(IHand hand, Seat aiSeat)
        {
            var solution = _playExtras.Solution;

            if (solution == null)
            {
                return ChooseFirstPlayableCard(hand, aiSeat);
            }

            var optimalPlays = solution.OptimalPlays(aiSeat);
            var optimalCards = optimalPlays as ICard[] ?? optimalPlays.ToArray();

            return optimalCards.Any()
                ? ChooseFirstPlayableCard(optimalCards, aiSeat)
                : ChooseFirstPlayableCard(hand, aiSeat);
        }

        private ICard ChooseFirstPlayableCard(IEnumerable<ICard> hand, Seat aiSeat)
        {
            return hand.FirstOrDefault(c => Game().CanFollow(c, aiSeat));
        }

        private IGame Game()
        {
            Debug.Assert(_session.Game != null, nameof(_session.Game) + " != null");
            return _session.Game;
        }

        private Seat DummySeat()
        {
            var auction = _session.Auction;
            System.Diagnostics.Debug.Assert(auction != null, "_session.Auction != null");
            System.Diagnostics.Debug.Assert(auction.FinalContract != null, "_session.Auction.FinalContract != null");
            return auction.FinalContract.Dummy();
        }
    }
}