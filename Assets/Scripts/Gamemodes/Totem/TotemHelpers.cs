using UnityEngine;

namespace Gamemodes.Totem
{
    public static class TotemHelpers
    {
        public static GameObject FindTotem()
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);

            if (totem)
            {
                return totem;
            }
            else
            {
                Debug.LogError("Totem was not found !");
                return null;
            }
        }

        public static TotemOwnership GetTotemComponent()
        {
            var totem = FindTotem();
            if (totem)
            {
                return totem.GetComponent<TotemOwnership>();
            }
            else
            {
                return null;
            }
        }

        public static BoltEntity GetTotemEntity()
        {
            var totem = FindTotem();
            if (totem)
            {
                return totem.GetComponent<BoltEntity>();
            }
            else
            {
                return null;
            }
        }

        public static int GetTotemOwnerID()
        {
            return GetTotemEntity().GetState<IItemState>().OwnerID;
        }
    }
}
