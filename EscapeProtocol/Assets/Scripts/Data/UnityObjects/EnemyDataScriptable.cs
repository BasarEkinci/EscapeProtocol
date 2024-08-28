using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "EscapeProtocol/EnemyData")]
    public class EnemyDataScriptable : ScriptableObject
    {
        public HealthData HealthData;
        public MovementData MovementData;
    }
}