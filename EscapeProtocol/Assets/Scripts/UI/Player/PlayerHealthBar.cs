using Controllers.Player;
using UnityEngine;

namespace UI.Player
{
    public class PlayerHealthBar : HealthBar
    {
        [SerializeField] private PlayerHealthController healthController;
        private void Start()
        {
            InitializeValues(healthController.Health);
        }
        
        private void Update()
        {
            UpdateValues(healthController.Health);
        }
    }
}