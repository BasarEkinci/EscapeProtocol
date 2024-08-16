using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "GunData", menuName = "EscapeProtocol/GunData", order = 0)]
    public class GunDataScriptable : ScriptableObject
    {
        public GunData GunData;
    }
}