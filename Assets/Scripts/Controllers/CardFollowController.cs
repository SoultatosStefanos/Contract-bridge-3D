using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Wrappers;
using Zenject;

namespace Controllers
{
    // TODO Handle dummy click
    public class CardFollowController : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        private CardWrapper _cardWrapper;

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
                ShowIllegalFollowToast();
            }
        }

        private void ShowIllegalFollowToast()
        {
            // TODO
        }
    }
}