using System;

namespace Base.UI
{
    [Serializable]
    public class TabGroup
    {
        public UITabButton button;
        public UITabPage page;

        public void Register(UITabControl control, int id) {
            button.Register(control, id);
            page.Register(control, id);
        }

        public void Init() {
            button.Init();
            page.Init();
        }

        public void Show() {
            button.Active();
            page.Active();
        }

        public void Hide() {
            button.Deactive();
            page.Deactive();
        }
    }
}