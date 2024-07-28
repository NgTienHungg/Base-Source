using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ViewPager
{
    public class UIViewPageTab : MonoBehaviour
    {
        [SerializeField] [GUIColor("green")]
        private int tabIndex;

        [SerializeField] private Image bgTabImg;
        [SerializeField] private Sprite bgNormalSpr, bgSelectedSpr;
        [SerializeField] private Animator animButtonTab;

        public void SetSelected(bool isSelected)
        {
            bgTabImg.sprite = isSelected ? bgSelectedSpr : bgNormalSpr;
            bgTabImg.SetNativeSize();
            animButtonTab.SetBool("Action", isSelected);
            Canvas.ForceUpdateCanvases();
        }

        public Vector3 GetPos()
        {
            return bgTabImg.rectTransform.position;
        }

        public Vector2 GetSizeDelta()
        {
            return bgTabImg.rectTransform.sizeDelta;
        }

        public void OnClick()
        {
            UIHomeViewPager.OnChangeTab?.Invoke(tabIndex);
        }
    }
}