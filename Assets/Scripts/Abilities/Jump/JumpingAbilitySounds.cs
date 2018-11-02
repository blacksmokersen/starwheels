using UnityEngine;

namespace Abilities.Jump
{
    public class JumpingAbilitySounds : MonoBehaviour
    {
        public AudioSource FirstJump;
        public AudioSource SecondJump;

        public void PlayFirstJumpSound()
        {
            FirstJump.Play();
        }

        public void PlaySecondJumpSound()
        {
            SecondJump.Play();
        }
    }
}
