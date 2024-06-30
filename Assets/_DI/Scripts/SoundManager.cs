using UnityEngine;

namespace _DI.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public void PlaySound() {
            Debug.Log("Playing sound".Color("green"));
        }

        public void StopSound() {
            Debug.Log("Stopping sound".Color("red"));
        }
    }
}