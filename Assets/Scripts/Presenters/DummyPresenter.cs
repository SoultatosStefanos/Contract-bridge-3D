using ContractBridge.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class DummyPresenter : MonoBehaviour
    {
        private TextMeshProUGUI _dummyText;

        [Inject]
        private ISession _session;

        private void Awake()
        {
            _dummyText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            if (DummySeat() is { } dummySeat)
            {
                UpdateVisual(dummySeat);
            }
        }

        private void UpdateVisual(Seat dummySeat)
        {
            _dummyText.text = $"Dummy: {dummySeat}";
        }

        private Seat? DummySeat()
        {
            return _session.Auction?.FinalContract?.Dummy();
        }
    }
}