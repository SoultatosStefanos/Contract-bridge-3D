using System;
using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

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

        private readonly Risk? _contractRisk = null; // No doubling/redoubling for this version!

        [Inject]
        private IAuctionExtras _auctionExtras;

        [Inject]
        private IContractFactory _contractFactory;

        private Denomination? _pickedDenomination;

        private Level? _pickedLevel;

        private Seat? _pickedSeat;

        public void PickedSeatChanged()
        {
            _pickedSeat = SeatByDropdownIndex(seatDropdown.value);

            if (_pickedSeat != null)
            {
                PickContractIfAllSet();
            }
            else
            {
                UnpickContract();
            }
        }

        public void PickedLevelChanged()
        {
            _pickedLevel = LevelByDropdownIndex(levelDropdown.value);

            if (_pickedLevel != null)
            {
                PickContractIfAllSet();
            }
            else
            {
                UnpickContract();
            }
        }

        public void PickedDenominationChanged()
        {
            _pickedDenomination = DenominationByDropdownIndex(denominationDropdown.value);

            if (_pickedDenomination != null)
            {
                PickContractIfAllSet();
            }
            else
            {
                UnpickContract();
            }
        }

        private void PickContractIfAllSet()
        {
            if (
                _pickedSeat is { } pickedSeat &&
                _pickedLevel is { } pickedLevel &&
                _pickedDenomination is { } pickedDenomination
            )
            {
                _auctionExtras.PickedContract = _contractFactory.Create(
                    pickedLevel,
                    pickedDenomination,
                    pickedSeat,
                    _contractRisk
                );
            }
        }

        private void UnpickContract()
        {
            _auctionExtras.PickedContract = null;
        }

        private static Seat? SeatByDropdownIndex(int index)
        {
            return index switch
            {
                0 => null,
                1 => Seat.South,
                2 => Seat.West,
                3 => Seat.North,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
        }

        private static Level? LevelByDropdownIndex(int index)
        {
            return index == 0 ? null : (Level)index;
        }

        private static Denomination? DenominationByDropdownIndex(int index)
        {
            return index == 0 ? null : (Denomination)index;
        }
    }
}