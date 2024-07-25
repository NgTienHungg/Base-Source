using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public enum EMessage
    {
        Notice,
        Confirm,
    }

    public class UIMessagePanel : UIPanel
    {
        [SerializeField]
        private Image imgPopup;

        [SerializeField]
        private TextMeshProUGUI txtTitle;

        [SerializeField]
        private TextMeshProUGUI txtContent;

        [SerializeField]
        private Button btnYes, btnNo, btnClose;

        public override bool CanBack => true;

        private UIMessagePanel SetTitle(string title)
        {
            txtTitle.text = title;
            return this;
        }

        private UIMessagePanel SetContent(string content)
        {
            txtContent.text = content;
            return this;
        }

        private UIMessagePanel SetLayout(EMessage type)
        {
            switch (type)
            {
                case EMessage.Notice:
                    btnYes.gameObject.SetActive(false);
                    btnNo.gameObject.SetActive(false);
                    btnClose.gameObject.SetActive(true);
                    break;

                case EMessage.Confirm:
                    btnYes.gameObject.SetActive(false);
                    btnNo.gameObject.SetActive(false);
                    btnClose.gameObject.SetActive(true);
                    break;
            }

            return this;
        }

        public void SetupNotice(string title, string content)
        {
            this.SetTitle(title)
                .SetContent(content)
                .SetLayout(EMessage.Notice);

            btnClose.onClick.AddListener(() => Hide().Forget());
        }

        public void SetupConfirm(string title, string content, Action onYes, Action onNo)
        {
            this.SetTitle(title)
                .SetContent(content)
                .SetLayout(EMessage.Confirm);

            btnYes.onClick.AddListener(() =>
            {
                onYes?.Invoke();
                Hide().Forget();
            });

            btnNo.onClick.AddListener(() =>
            {
                onNo?.Invoke();
                Hide().Forget();
            });
        }
    }
}