using System.ComponentModel;
using Controller;
using UnityEngine;

public partial class SROptions
{
    [Category("Resource")]
    public int Gold
    {
        get => GameManager.Instance.Gold;
        set => GameManager.Instance.Gold = value;
    }

    [Category("Resource")]
    public int Gem
    {
        get => GameManager.Instance.Gold;
        set => GameManager.Instance.Gold = value;
    }

    [Category("Resource")]
    public bool AdsRemoved
    {
        get => GameManager.Instance.IsAdsRemoved;
        set => GameManager.Instance.IsAdsRemoved = value;
    }

    [Category("Game")]
    public void GameOver()
    {
        Debug.Log("GameOver!".Color("cyan"));
    }
}