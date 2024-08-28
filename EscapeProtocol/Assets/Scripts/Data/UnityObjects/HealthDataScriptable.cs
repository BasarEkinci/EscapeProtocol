using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "HealthData", menuName = "EscapeProtocol/HealthData")]
    public class HealthDataScriptable : ScriptableObject
    {
        public int MaxHealth;
    }
}