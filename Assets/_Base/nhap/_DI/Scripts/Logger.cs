using UnityEngine;

namespace _DI.Scripts
{
    public class Logger : ILogger
    {
        private static int createdCount = 0;

        public Logger() {
            Debug.Log("Logger created " + (++createdCount));
        }

        public void Log(string message) {
            Debug.Log(message);
        }
    }
}