using UnityEngine;
using Zenject;

namespace _DI.Scripts
{
    public class Player : MonoBehaviour
    {
        [Inject] private Logger logger;
        // [Inject] private SoundManager soundManager;
    
        private void Awake() {
            // logger.Log("Player Awake".Color("cyan"));
        }

        public void Start() {
            logger.Log("Start at Player".Color("cyan"));
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                logger.Log("Space key pressed".Color("yellow"));
                // soundManager.PlaySound();
            }
        }
    }
}