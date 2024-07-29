using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Base.LoadScene
{
    public class UILoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI progressText;

        private float currentPercent;

        public void SetProgress(float progress) {
            DOVirtual.Float(
                currentPercent, progress.Percent(), 0.5f,
                x => {
                    currentPercent = x;
                    progressText.text = x.Int() + "%";
                });
        }

        public void Show() {
            gameObject.SetActive(true);
            progressText.text = "0%";
            currentPercent = 0;
        }

        public void Hide() {
            GetComponent<Animator>().SetTrigger($"Close");
        }

        public void Deactive() {
            gameObject.SetActive(false);
        }
    }
}