using ContractBridge.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class ContractPickController : MonoBehaviour
    {
        [FormerlySerializedAs("Seat Dropdown")]
        [SerializeField]
        private TMP_Dropdown seatDropdown;

        [FormerlySerializedAs("Level Dropdown")]
        [SerializeField]
        private TMP_Dropdown levelDropdown;

        [FormerlySerializedAs("Denomination Dropdown")]
        [SerializeField]
        private TMP_Dropdown denominationDropdown;

        private Denomination? _pickedDenomination;

        private Level? _pickedLevel;

        private Seat? _pickedSeat;

        public void PickedSeatChanged()
        {
            Debug.Log($"Index of picked seat: {seatDropdown.value}");
        }

        public void PickedLevelChanged()
        {
            Debug.Log($"Index of picked level: {levelDropdown.value}");
        }

        public void PickedDenominationChanged()
        {
            Debug.Log($"Index of picked denomination: {denominationDropdown.value}");
        }
    }
}