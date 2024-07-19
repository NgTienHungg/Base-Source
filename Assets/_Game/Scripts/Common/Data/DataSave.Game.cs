using Game.DinoPass;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

public partial class DataSave
{
    [GUIColor("yellow")]
    public DinoPassDataSave DinoPass;
    
    private void LoadData()
    {
        Debug.Log("[Data] Load save data...".Color("orange"));
        DinoPass = JsonConvert.DeserializeObject<DinoPassDataSave>(GameConfig.FilePath.DinoPassDataSave);
    }
}