using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.DinoPass
{
    public class UIPickaxeProgressBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stageTxt;
        [SerializeField] private TextMeshProUGUI progressTxt;
        [SerializeField] private Image progressFill;

        private DinoPassDataConfig dataConfig;
        private DinoPassDataSave dataSave;

        private async void OnEnable()
        {
            // wait 1 frame for handle pickaxe in PopupDinoPass
            await UniTask.DelayFrame(1);

            // var dataSave = ProfileManager.Instance.UserData.DinoPassDataSave;
            // var dataConfig = GameManager.Instance.DinoPassDataConfig;

            var currentPickaxes = dataSave.CurrentPickaxes;
            var currentStageId = dataSave.CurrentStageId;
            var currentPickaxeRequired = dataConfig.GetPickaxeRequiredAtStage(currentStageId);

            stageTxt.text = (currentStageId + 1).ToString();
            progressTxt.text = $"{currentPickaxes}/{currentPickaxeRequired}";
            progressFill.fillAmount = 1f * currentPickaxes / currentPickaxeRequired;
        }
    }
}