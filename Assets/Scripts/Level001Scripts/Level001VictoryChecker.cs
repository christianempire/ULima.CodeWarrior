using Assets.Scripts.SharedLevelScripts;
using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Level001Scripts
{
    public class Level001VictoryChecker : VictoryChecker
    {
        public Tilemap CheckpointsTilemap;
        public CheckpointSeeker CheckpointSeeker;

        #region Properties
        private GameModel gameModel;
        private Vector2 victoryCheckpointPosition;
        #endregion

        void Awake()
        {
            gameModel = Schedule.GetModel<GameModel>();

            LocateVictoryCheckpointPosition();
        }

        public override bool IsVictoryAchieved()
        {
            return IsAppleCollected() && IsVictoryCheckpointReached();

            bool IsAppleCollected() => gameModel.GetInventoryCount("Apple") == 1;
            bool IsVictoryCheckpointReached() => CheckpointSeeker.GetCurrentPosition() == victoryCheckpointPosition;
        }

        #region Helpers
        private void LocateVictoryCheckpointPosition()
        {
            const string VictoryCheckpointTileName = "Checkpoint2";

            foreach (var position in CheckpointsTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(position.x, position.y, position.z);
                var place = CheckpointsTilemap.CellToWorld(localPlace);
                var tile = CheckpointsTilemap.GetTile(localPlace);

                if (tile != null && tile.name.Equals(VictoryCheckpointTileName))
                {
                    victoryCheckpointPosition = place;
                    break;
                }
            }
        }
        #endregion
    }
}
