using System;
using System.Collections.Generic;
using System.Linq;
using Buttons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Groups
{
    public class ToggleButtonGroup : MonoBehaviour
    {
        [FormerlySerializedAs("Toggled")]
        [SerializeField]
        public ToggleEvent toggled = new();

        private readonly List<ToggleButton> _toggles = new();

        // NOTE: This is the sort-of "Main" toggle button.
        // It may be either checked, when the player toggles a button in the group, or unchecked, if the player just
        // unchecks the active toggle.
        private ToggleButton _toggle;

        public ToggleButton Toggle
        {
            get => _toggle;
            private set
            {
                _toggle = value;
                toggled.Invoke(Toggle);
            }
        }

        public void RegisterToggle(ToggleButton toggle)
        {
            _toggles.Add(toggle);

            toggle.Toggled.AddListener(OnToggled);
        }

        private void OnToggled(ToggleButton toggle)
        {
            if (!toggle.Checked)
            {
                if (ReferenceEquals(Toggle, toggle))
                {
                    Toggle = toggle; // NOTE: Only to trigger the event, don't sweat it. :)
                }

                return;
            }

            foreach (var item in _toggles.Where(item => item.GetInstanceID() != toggle.GetInstanceID()))
            {
                item.Checked = false;
            }

            Toggle = toggle;
        }

        [Serializable]
        public class ToggleEvent : UnityEvent<ToggleButton>
        {
        }
    }
}