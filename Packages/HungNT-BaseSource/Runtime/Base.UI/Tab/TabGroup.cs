using System;
using Cysharp.Threading.Tasks;

namespace Base
{
    [Serializable]
    public class TabGroup
    {
        public UITabButton button;
        public UITabPage page;

        public void Register(UITabControl control, int id)
        {
            button.Register(control, id);
            page.Register(control, id);
        }

        public UniTask Init()
        {
            button.Init();
            return page.Init();
        }

        public UniTask Show()
        {
            button.Active();
            page.Active();
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            button.Deactive();
            page.Deactive();
            return UniTask.CompletedTask;
        }
    }
}