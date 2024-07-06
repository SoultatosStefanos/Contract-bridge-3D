using System.Collections;
using System.Linq;
using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace AI
{
    // TODO Handle Dummy, Optimal moves, end.

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
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<GameTurnChangeEvent>(HandleGameTurnChange);

            if (_session.Game?.Turn is not { } turn)
            {
                return;
            }

            // TODO Work with dummy too...
            if (turn == playerSeat)
            {
                return;
            }

            StartCoroutine(TakeTurn(turn));
        }

        private void OnDisable()
        {
            _eventBus.Off<GameTurnChangeEvent>(HandleGameTurnChange);
        }

        private void HandleGameTurnChange(GameTurnChangeEvent e)
        {
            // TODO Work with dummy too...
            if (e.Seat == playerSeat)
            {
                return;
            }

            StartCoroutine(TakeTurn(e.Seat));
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
            // TODO Change this, this should take optimal move. 

            return hand.FirstOrDefault(c => Game().CanFollow(c, aiSeat));
        }

        private IGame Game()
        {
            Debug.Assert(_session.Game != null, nameof(_session.Game) + " != null");
            return _session.Game;
        }
    }
}