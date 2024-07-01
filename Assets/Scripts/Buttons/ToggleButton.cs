using System;
using Groups;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Image))]
    public class ToggleButton : MonoBehaviour, IPointerClickHandler
    {
        [FormerlySerializedAs("Toggled")]
        [SerializeField]
        public ToggleEvent toggled = new();

        [FormerlySerializedAs("Checked Color")]
        [SerializeField]
        private Color checkedColor;

        [FormerlySerializedAs("Group")]
        [SerializeField]
        private ToggleButtonGroup group;

        private bool _checked;

        private Image _image;

        private Color _originalColor;

        public ToggleEvent Toggled => toggled;

        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked == value)
                {
                    return;
                }

                _checked = value;
                UpdateVisual();
                toggled.Invoke(this);
            }
        }

        private void Start()
        {
            _image = GetComponent<Image>();
            _originalColor = _image.color;

            if (group != null)
            {
                group.RegisterToggle(this);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Checked = !Checked;
        }

        private void UpdateVisual()
        {
            _image.color = Checked ? checkedColor : _originalColor;
        }

        [Serializable]
        public class ToggleEvent : UnityEvent<ToggleButton>
        {
        }
    }
}