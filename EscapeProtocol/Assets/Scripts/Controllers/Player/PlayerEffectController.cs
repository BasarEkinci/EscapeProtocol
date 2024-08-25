using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerEffectController : MonoBehaviour
    {
        [Header("Particle Effects")] 
        [SerializeField] private List<ParticleSystem> jumpParticles;
        [SerializeField] private List<ParticleSystem> landParticles;
        
        
        
        internal void SetParticles(bool isGrounded)
        {
            if (!isGrounded)
            {
                foreach (var particle in jumpParticles.Where(particle => particle.isStopped))
                {
                    particle.Play();
                }
                foreach (var particle in landParticles.Where(particle => !particle.isPlaying))
                {
                    particle.Play();
                }
            }
            else
            {
                foreach (var particle in landParticles.Where(particle => particle.isPlaying))
                {
                    particle.Stop();
                }
            }
        }
    }
}
