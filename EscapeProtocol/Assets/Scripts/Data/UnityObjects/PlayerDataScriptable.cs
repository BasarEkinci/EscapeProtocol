using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "EscapeProtocol/PlayerData")]
    public class PlayerDataScriptable : ScriptableObject
    {
        public HealthData HealthData;
        public MovementData MovementData;
    }
}