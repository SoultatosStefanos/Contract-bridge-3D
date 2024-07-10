using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Wrappers;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Controllers
{
    public class CardFollowController : MonoBehaviour
    {
        private const string IllegalFollowErrorMsg = "Can't play card!";

        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        private CardWrapper _cardWrapper;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void Awake()
        {
            _cardWrapper = GetComponent<CardWrapper>();
        }

        private void Start()
        {
            // NOTE: Only to deactivate this from the inspector.
        }

        private void OnMouseDown()
        {
            // NOTE: Disabled components still callback functions like these!
            // The flag only affects Start() and Update().
            if (!enabled)
            {
                return;
            }

            if (_session.Game is not { } game)
            {
                return;
            }

            var card = _cardWrapper.Card;

            // NOTE: This is an exceptional situation on the backend, but it may just happen given our setup.

            if (!_session.Board.Hand(PlayedSeat()).Contains(card))
            {
                FireIllegalFollowError();
                return;
            }

            if (game.CanFollow(card, PlayedSeat()))
            {
                game.Follow(card, PlayedSeat());
            }
            else
            {
                FireIllegalFollowError();
            }
        }

        private void FireIllegalFollowError()
        {
            _eventBus.Post(new ErrorEvent(IllegalFollowErrorMsg));
        }

        // NOTE: Always assume player seat play, except when it's partner's turn, and he is the dummy.
        private Seat PlayedSeat()
        {
            Debug.Assert(_session.Game != null, "_session.Game != null");

            var turnSeat = _session.Game.Turn;
            var partnerSeat = playerSeat.Partner();
            var dummySeat = DummySeat();

            return turnSeat == partnerSeat && partnerSeat == dummySeat ? partnerSeat : playerSeat;
        }

        private Seat DummySeat()
        {
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            Debug.Assert(_session.Auction.FinalContract != null, "_session.Auction.FinalContract != null");
            return _session.Auction.FinalContract.Dummy();
        }
    }
}