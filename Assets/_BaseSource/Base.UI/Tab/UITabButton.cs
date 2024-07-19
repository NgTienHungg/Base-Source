using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Base.UI
{
    public class UITabButton : UIButtonBase
    {
        [Space] [SerializeField]
        protected bool useBackground = true;

        [SerializeField] [ShowIf("@useBackground")]
        protected Image bgImg;

        [SerializeField] [ShowIf("@useBackground")]
        protected Sprite onBgSprite, offBgSprite;

        [Space] [SerializeField]
        protected bool useIcon;

        [SerializeField] [ShowIf("@useIcon")]
        protected Image iconImg;

        [SerializeField] [ShowIf("@useIcon")]
        protected Sprite onIconSprite, offIconSprite;

        [Space] [SerializeField]
        protected bool useText;

        [SerializeField] [ShowIf("@useText")]
        protected TextMeshProUGUI textTMP;

        [SerializeField] [ShowIf("@useText")]
        protected Color onTextColor, offTextColor;

        protected UITabControl control;
        protected int id;

        protected override void Reset()
        {
            base.Reset();
            bgImg = GetComponent<Image>();
        }

        public void Register(UITabControl control, int id)
        {
            this.control = control;
            this.id = id;
        }

        public virtual void Init()
        { }

        public virtual void Active()
        {
            if (useBackground)
            {
                bgImg.sprite = onBgSprite;
            }

            if (useIcon)
            {
                iconImg.sprite = onIconSprite;
            }

            if (useText)
            {
                textTMP.color = onTextColor;
            }
        }

        public virtual void Deactive()
        {
            if (useBackground)
            {
                bgImg.sprite = offBgSprite;
            }

            if (useIcon)
            {
                iconImg.sprite = offIconSprite;
            }

            if (useText)
            {
                textTMP.color = offTextColor;
            }
        }

        protected override void OnClick()
        {
            control.OpenTab(id);
        }
    }
}