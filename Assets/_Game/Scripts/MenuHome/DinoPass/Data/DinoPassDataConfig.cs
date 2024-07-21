using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DinoPass
{
    [CreateAssetMenu(menuName = "Game/Config/Dino Pass", fileName = "DinoPassDataConfig")]
    public class DinoPassDataConfig : ScriptableObject
    {
        // [TableList]
        public List<DinoPassStage> Stages;

        public int StageCount => Stages.Count;

        public int GetPickaxeRequiredAtStage(int stageId)
        {
            return Stages[stageId].PickaxeCount;
        }

        public int GetTotalPickaxeRequiredAtStage(int stageId)
        {
            var pickaxeRequired = 0;
            foreach (var stage in Stages)
            {
                if (stage.Id > stageId) break;
                pickaxeRequired += stage.PickaxeCount;
            }

            return pickaxeRequired;
        }

        public Sprite GetRewardSprite(EDinoReward rewardType)
        {
            // return RewardSprites[(int)rewardType];
            return null;
        }
    }
}