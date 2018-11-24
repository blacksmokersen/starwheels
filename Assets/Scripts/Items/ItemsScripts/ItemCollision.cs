﻿using UnityEngine;

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
            else if (itemCollision.ItemName == ItemCollisionName.IonBeam && IonBeam)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.Totem && Totem)
            {
                return true;
            }
            else if (itemCollision.ItemName == ItemCollisionName.Kart && Kart)
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