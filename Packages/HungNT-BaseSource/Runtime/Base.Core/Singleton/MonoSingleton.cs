using UnityEngine;

public abstract class MonoSingleton<TMono> : MonoBehaviour where TMono : MonoBehaviour
{
    private static TMono _instance;

    public static TMono Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TMono>();
                if (_instance == null)
                {
                    Debug.Log("Create new instance of " + typeof(TMono).Name.Color("yellow"));
                    var go = new GameObject(typeof(TMono).Name);
                    _instance = go.AddComponent<TMono>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as TMono;
            OnAwake();
            return;
        }

        Destroy(gameObject);
    }

    protected abstract void OnAwake();
}