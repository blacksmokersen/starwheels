using UnityEngine;

namespace GameModes.Totem
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

        public static Totem GetTotemComponent()
        {
            return FindTotem().GetComponent<Totem>();
        }

        public static BoltEntity GetTotemEntity()
        {
            return FindTotem().GetComponent<BoltEntity>();
        }

        public static int GetTotemOwnerID()
        {
            return GetTotemEntity().GetState<IItemState>().OwnerID;
        }
    }
}
