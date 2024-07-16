using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game.DinoPass
{
    [Serializable]
    public class DinoPassDataConfig
    {
        [TableList]
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
    }
}