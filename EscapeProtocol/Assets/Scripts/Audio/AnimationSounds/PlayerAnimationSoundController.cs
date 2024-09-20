namespace Audio.AnimationSounds
{
    public class PlayerAnimationSoundController : AnimationSoundPlayer
    {
        public void Walk()
        {
            AudioSource.PlayOneShot(soundClips[0]);
        }
    }
}