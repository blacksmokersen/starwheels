using UnityEngine;

public class JumpingAbilitySounds : MonoBehaviour
{
    public AudioSource FirstJump;
    public AudioSource SecondJump;

    public void PlayFirstJumpSound()
    {
        FirstJump.Play();
        Debug.Log("Played");
    }

    public void PlaySecondJumpSound()
    {
        SecondJump.Play();
    }
}
