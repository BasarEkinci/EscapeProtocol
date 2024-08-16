using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "EscapeProtocol/SoundData")]
    public class SoundDataScriptable : ScriptableObject
    {
        public SoundList[] sounds;
    }
}
