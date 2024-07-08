using System.Linq;
using ContractBridge.Core;
using Events;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters
{
    public class TricksPresenter : MonoBehaviour
    {
        [FormerlySerializedAs("Partnership")]
        [SerializeField]
        private Partnership partnership;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private TextMeshProUGUI _tricksText;

        private void Awake()
        {
            _tricksText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus.On<PairTrickWonEvent>(HandlePairTrickWonEvent);

            var tricksMade = _session.Pair(partnership).AllTricksWon.Count();
            UpdateVisual(tricksMade);
        }

        private void OnDisable()
        {
            _eventBus.Off<PairTrickWonEvent>(HandlePairTrickWonEvent);
        }

        private void HandlePairTrickWonEvent(PairTrickWonEvent e)
        {
            if (e.Pair.Partnership != partnership)
            {
                return;
            }

            var tricksMade = e.Pair.AllTricksWon.Count();
            UpdateVisual(tricksMade);
        }

        private void UpdateVisual(int tricksMade)
        {
            _tricksText.text = $"Tricks ({partnership.ToShortHandString()}): {tricksMade}";
        }
    }
}