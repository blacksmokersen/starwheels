using UnityEngine;

namespace Items
{
    public enum ItemCollisionName
    {
        Disk,
        Rocket,
        Mine,
        Guile,
        IonBeam,
        Totem,
        Kart
    }

    [CreateAssetMenu(menuName ="Item/ItemCollision")]
    public class ItemCollision : ScriptableObject
    {
        public ItemCollisionName ItemName;

        [Header("Destroy myself when hitting ...")]
        public bool Disk;
        public bool Rocket;
        public bool Mine;
        public bool Guile;
        public bool IonBeam;
        public bool Totem;
        public bool Kart;

        public void CheckCollision(ItemCollision itemCollision)
        {
            if (itemCollision.ItemName == ItemCollisionName.Disk && Disk)
            {

            }
            else if (itemCollision.ItemName == ItemCollisionName.Rocket && Rocket)
            {

            }
            else if (itemCollision.ItemName == ItemCollisionName.Guile && Guile)
            {

            }
            else if (itemCollision.ItemName == ItemCollisionName.IonBeam && IonBeam)
            {

            }
            else if (itemCollision.ItemName == ItemCollisionName.Totem && Totem)
            {

            }
            else if (itemCollision.ItemName == ItemCollisionName.Kart && Kart)
            {

            }
            else
            {
                Debug.LogError("Unknown ItemCollision name.");
            }            
        }
    }
}