using Assets.Scripts.Constants;
using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Level001
{
    public class Level001VictoryChecker : VictoryChecker
    {
        public Tilemap CheckpointsTilemap;
        public GameObject Hero;

        #region Properties
        private int totalItems;
        private Vector2 victoryCheckpointPosition;
        #endregion

        void Awake()
        {
            FetchTotalItems();
            LocateVictoryCheckpointPosition();
        }

        public override bool IsVictoryAchieved()
        {
            return HasCollectedAllItems() && IsInVictoryCheckpoint();

            bool HasCollectedAllItems() => Hero.GetComponent<ItemPicker>().ItemsCount == totalItems;
            bool IsInVictoryCheckpoint() => Hero.GetComponent<CheckpointSeeker>().GetCurrentPosition() == victoryCheckpointPosition;
        }

        #region Helpers
        private void FetchTotalItems()
        {
            totalItems = GameObject.FindGameObjectsWithTag(TagConstants.ItemTag).Length;
        }

        private void LocateVictoryCheckpointPosition()
        {
            foreach (var position in CheckpointsTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(position.x, position.y, position.z);
                var place = CheckpointsTilemap.CellToWorld(localPlace);
                var tile = CheckpointsTilemap.GetTile(localPlace);

                if (tile != null && tile.name.Equals(TileConstants.VictoryCheckpointTile))
                {
                    victoryCheckpointPosition = place;
                    break;
                }
            }
        }
        #endregion
    }
}
