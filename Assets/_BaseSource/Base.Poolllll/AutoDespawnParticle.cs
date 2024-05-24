using UnityEngine;

namespace Base.Pool
{
    /// <summary>
    /// Gán script này vào Particle có thuộc Pool
    /// Sau đó chọn "StopAction" = "Callback"
    /// </summary>
    public class AutoDespawnParticle : MonoBehaviour
    {
        private void OnParticleSystemStopped()
        {
            // PoolManager.Instance.Despawn(gameObject);
        }
    }
}