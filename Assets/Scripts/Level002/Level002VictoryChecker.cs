using Assets.Scripts.Constants;
using Assets.Scripts.Shared;
using Assets.Scripts.Shared.Hero;
using Assets.Scripts.Shared.Level;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Level002
{
    public class Level002VictoryChecker : VictoryChecker
    {
        public Tilemap CheckpointsTilemap;
        public GameObject Hero;

        #region Properties
        private ItemPicker heroItemPicker;
        private PositionableEntity heroPositionableEntity;
        private int totalItems;
        #endregion

        void Awake()
        {
            InitializeProperties();
        }

        public override bool IsVictoryAchieved()
        {
            var victoryCheckpointPosition = GetVictoryCheckpointPosition();

            return HasCollectedAllItems() && IsInVictoryCheckpoint();

            bool HasCollectedAllItems() => heroItemPicker.ItemsCount == totalItems;
            bool IsInVictoryCheckpoint() => heroPositionableEntity.GetPosition() == victoryCheckpointPosition;
        }

        #region Helpers
        private Vector2 GetVictoryCheckpointPosition()
        {
            foreach (var position in CheckpointsTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(position.x, position.y, position.z);
                var place = CheckpointsTilemap.CellToWorld(localPlace);
                var tile = CheckpointsTilemap.GetTile(localPlace);

                if (tile != null && tile.name.Equals(TileConstants.VictoryCheckpointTile))
                    return place;
            }

            throw new NoNullAllowedException();
        }

        private void InitializeProperties()
        {
            heroItemPicker = Hero.GetComponent<ItemPicker>();
            heroPositionableEntity = Hero.GetComponent<PositionableEntity>();
            totalItems = GameObject.FindGameObjectsWithTag(TagConstants.ItemTag).Length;
        }
        #endregion
    }
}
