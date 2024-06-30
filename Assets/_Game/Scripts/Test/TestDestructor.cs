using UnityEngine;

namespace Test
{
    public class TestDestructor : MonoBehaviour
    {
        private Bird bird;

        private void Start() {
            bird = new Bird();
        }
    }
}