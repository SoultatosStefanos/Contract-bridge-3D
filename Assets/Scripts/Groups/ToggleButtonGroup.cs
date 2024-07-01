using System.Collections.Generic;
using System.Linq;
using Buttons;
using UnityEngine;

namespace Groups
{
    public class ToggleButtonGroup : MonoBehaviour
    {
        private readonly List<ToggleButton> _toggles = new();

        public void RegisterToggle(ToggleButton toggle)
        {
            _toggles.Add(toggle);
            toggle.Toggled.AddListener(HandleCheckedChanged);
        }

        private void HandleCheckedChanged(ToggleButton toggle)
        {
            if (!toggle.Checked)
            {
                return;
            }

            foreach (var item in _toggles.Where(item => item.GetInstanceID() != toggle.GetInstanceID()))
            {
                item.Checked = false;
            }
        }
    }
}