using UnityEngine;
using System;

namespace GameDatabase
{
    [Serializable]
    public class DatabaseEnemyTypes
    {
        public BaseGampelayObjectDataAndStats[] EnemiesTypesDataAll => enemiesTypesDataAll;

        [SerializeField] private BaseGampelayObjectDataAndStats[] enemiesTypesDataAll;
    }
}
