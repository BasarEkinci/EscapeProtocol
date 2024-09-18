namespace Audio
{
    public class EnemyAnimationSoundController : AnimationSoundPlayer
    {
        public void FootStep1()
        {
            AudioSource.PlayOneShot(soundClips[0]);
        }
        public void FootStep2()
        {
            AudioSource.PlayOneShot(soundClips[1]);
        }
    }
}