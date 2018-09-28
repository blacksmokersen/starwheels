using UnityEngine;

[CreateAssetMenu(menuName = "Sounds/JumpingAbility")]
public class JumpingAbilitySounds : ScriptableObject
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
