using UnityEngine;

namespace Items
{
    public enum ItemCollisionName
    {
        Disk,
        Rocket,
        Mine,
        Guile,
        IonBeamLaser,
        GoldDisk,
        Laser,
        Totem,
        ItemDestroyer
    }

    [CreateAssetMenu(menuName ="Item/ItemCollision")]
    public class ItemCollision : ScriptableObject
    {
        public ItemCollisionName ItemName;

        [Header("Player Collision")]
        public bool HitsPlayer;

        [Header("Destroy myself when hitting ...")]
        public bool Disk;
        public bool Rocket;
        public bool Mine;
        public bool Guile;
        public bool IonBeamLaser;
        public bool GoldDisk;
        public bool Laser;
        public bool Totem;
        public bool ItemDestroyer;

        public bool ShouldBeDestroyed(ItemCollision itemCollision)
        {
            if (itemCollision.ItemName == ItemCollisionName.Disk && Disk)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.Rocket && Rocket)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.Mine && Mine)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.Guile && Guile)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.IonBeamLaser && IonBeamLaser)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.GoldDisk && GoldDisk)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.Laser && Laser)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.Totem && Totem)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.ItemDestroyer && ItemDestroyer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
