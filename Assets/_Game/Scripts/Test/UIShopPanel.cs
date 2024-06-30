using Base.Data;
using UnityEngine;
using Zenject;

public class UIShopPanel : MonoBehaviour
{
    public DataManager _dataManager;

    private bool printed = false;

    [Inject]
    private void Construct(DataManager dataManager) {
        _dataManager = dataManager;
    }

    private void Update() {
        if (_dataManager != null && !printed) {
            Debug.Log("injected DataManager".Color("yellow"));
            printed = true;
        }
    }
}