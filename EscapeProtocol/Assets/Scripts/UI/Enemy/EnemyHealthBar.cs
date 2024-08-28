using Controllers.Enemy;
using UnityEngine;

namespace UI.Enemy
{
    public class EnemyHealthBar : HealthBar
    {
        [SerializeField] private EnemyHealthController healthController;

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
