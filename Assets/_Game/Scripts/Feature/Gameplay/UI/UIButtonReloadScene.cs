using Base.UI;
using UnityEngine.SceneManagement;

namespace Feature.Gameplay
{
    public class UIButtonReloadScene : UIButtonBase
    {
        protected override void OnClick() {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}