using Assets.Scripts.Constants;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Shared.Level.VictoryConditions
{
    public class VictoryCheckpointVictoryCondition : VictoryCondition
    {
        public GameObject Hero;
        public Tilemap CheckpointsTilemap;

        #region Properties
        private PositionableEntity heroPositionableEntity;
        private Vector2 victoryCheckpointPosition;
        #endregion

        public void Awake()
        {
            InitializeProperties();
        }

        public override async Task<bool> IsMetAsync() => await Task.FromResult(heroPositionableEntity.GetPosition() == victoryCheckpointPosition);

        #region Helpers
        private Vector2 GetVictoryCheckpointPosition(Tilemap checkpointsTilemap)
        {
            foreach (var position in checkpointsTilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(position.x, position.y, position.z);
                var place = checkpointsTilemap.CellToWorld(localPlace);
                var tile = checkpointsTilemap.GetTile(localPlace);

                if (tile != null && tile.name.Equals(TileConstants.VictoryCheckpointTile))
                    return place;
            }

            throw new NoNullAllowedException();
        }

        private void InitializeProperties()
        {
            heroPositionableEntity = Hero.GetComponent<PositionableEntity>();
            victoryCheckpointPosition = GetVictoryCheckpointPosition(CheckpointsTilemap);
        }
        #endregion
    }
}
