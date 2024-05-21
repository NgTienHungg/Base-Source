using UnityEngine;

namespace Base.UI
{
    public class UITabPage : MonoBehaviour
    {
        protected UITabControl control;
        protected int id;

        public void Register(UITabControl control, int id) {
            this.control = control;
            this.id = id;
        }

        public virtual void Init() { }

        public virtual void Active() {
            gameObject.SetActive(true);
        }

        public virtual void Deactive() {
            gameObject.SetActive(false);
        }
    }
}