using Assets.Scripts.Shared;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Level001
{
    public class Level001VictoryChecker : VictoryChecker
    {
        public Tilemap CheckpointsTilemap;
        public CheckpointSeeker CheckpointSeeker;

        #region Properties
        private Vector2 victoryCheckpointPosition;
        #endregion

        void Awake()
        {
            LocateVictoryCheckpointPosition();
        }

        public override bool IsVictoryAchieved()
        {
            return CheckpointSeeker.GetCurrentPosition() == victoryCheckpointPosition;
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
