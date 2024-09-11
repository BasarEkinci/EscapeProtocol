using UnityEngine.Events;
using Utilities;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction OnPlayerDeath = delegate { };
    }
}