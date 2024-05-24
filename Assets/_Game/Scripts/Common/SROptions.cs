using System.ComponentModel;
using Controller;

public partial class SROptions
{
    [Category("Resource")]
    public int Gold
    {
        get => GameManager.Instance.Gold;
        set => GameManager.Instance.Gold = value;
    }
}