using System.Collections;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters
{
    public class ErrorMessagePresenter : MonoBehaviour
    {
        [FormerlySerializedAs("Error Panel")]
        [SerializeField]
        private GameObject errorPanel;

        [FormerlySerializedAs("Display Duration")]
        [SerializeField]
        [Tooltip("Duration in seconds to display the message.")]
        private float displayDuration = 2f;

        [FormerlySerializedAs("Fade Duration")]
        [SerializeField]
        [Tooltip("Duration in seconds for fade-in and fade-out.")]
        private float fadeDuration = 0.5f;

        [FormerlySerializedAs("Error Text")]
        [SerializeField]
        private TextMeshProUGUI errorText;

        private CanvasGroup _canvasGroup;

        private Coroutine _currentCoroutine;

        [Inject]
        private IEventBus _eventBus;

        private void Start()
        {
            errorPanel.SetActive(false);

            _canvasGroup = errorPanel.GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = errorPanel.AddComponent<CanvasGroup>();
            }

            _canvasGroup.alpha = 0f;
        }

        private void OnEnable()
        {
            _eventBus.On<ErrorEvent>(HandleErrorEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<ErrorEvent>(HandleErrorEvent);
        }

        private void HandleErrorEvent(ErrorEvent e)
        {
            ShowError(e.Message);
        }

        private void ShowError(string message)
        {
            errorText.text = message;

            errorPanel.SetActive(true);

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(FadeError(true));

            StartCoroutine(HideErrorAfterDelay());
        }

        private IEnumerator FadeError(bool fadeIn)
        {
            var startAlpha = fadeIn ? 0f : 1f;
            var endAlpha = fadeIn ? 1f : 0f;
            var elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _canvasGroup.alpha = endAlpha;
        }

        private IEnumerator HideErrorAfterDelay()
        {
            yield return new WaitForSeconds(displayDuration);

            yield return StartCoroutine(FadeError(false));

            errorPanel.SetActive(false);
        }
    }
}