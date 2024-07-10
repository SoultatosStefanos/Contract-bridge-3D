using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Wrappers;
using Zenject;

namespace Controllers
{
    // TODO Handle dummy click
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

            if (game.CanFollow(card, playerSeat))
            {
                game.Follow(card, playerSeat);
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
    }
}