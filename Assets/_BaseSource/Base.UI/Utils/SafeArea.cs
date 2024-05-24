using UnityEngine;

namespace Base.UI
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField]
        private Rect rect;

        // Start is called before the first frame update
        private void Awake() {
            Safe();
        }

        public void Safe() {
            Canvas canvas = GetComponentInParent<Canvas>();
            rect = Screen.safeArea;
            rect.size /= canvas.scaleFactor;
            RectTransform rt = GetComponent<RectTransform>();

            var safeArea = Screen.safeArea;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= canvas.pixelRect.width;
            anchorMin.y /= canvas.pixelRect.height;
            anchorMax.x /= canvas.pixelRect.width;
            anchorMax.y /= canvas.pixelRect.height;

            //Logger.Log("SA "+safeArea.size + " " + canvas.pixelRect.size + " "+canvas.scaleFactor);
            //Logger.Log("SA "+safeArea.position );

            anchorMin.y = 0;
            anchorMax.y = 1;
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
        }
    }
}