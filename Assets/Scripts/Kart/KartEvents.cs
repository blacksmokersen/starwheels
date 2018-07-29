using System;
using Items;

public static class KartEvents
{
	public static Action<float> OnVelocityChange;
    public static Action<ItemData,int> OnItemUsed;
    public static Action OnHit;

    // Jumping Capacity
    public static Action OnJump;
    public static Action<Directions> OnDoubleJump;
    public static Action OnDoubleJumpReset;
}
