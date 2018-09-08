public class Constants
{
    // Layers
    public class Layer
    {
        public const string Ground = "Ground";
    }

    // Tags
    public class Tag
    {
        public const string Spawn = "Spawn";
        public const string Kart = "Kart";
        public const string KartTrigger = "KartTrigger";
        public const string KartColliderTag = "KartCollider";
        public const string ItemBox = "ItemBox";
        public const string GroundItem = "GroundItem";
        public const string GuileItem = "GuileItem";
        public const string RocketItem = "Rocket";
        public const string DiskItem = "Disk";
    }

    // Inputs
    public class Input
    {
        public const string Accelerate = "Accelerate";
        public const string Decelerate = "Decelerate";
        public const string Fire = "Fire";
        public const string TurnAxis = "Turn";
        public const string UpAndDownAxis = "UpAndDown";
        public const string Drift = "Drift";
        public const string UseItem = "Item";
        public const string UseItemForward = "ItemForward";
        public const string UseItemBackward = "ItemBackward";
        public const string UseAbility = "Ability";
        public const string TurnCamera = "RightJoystick";
        public const string BackCamera = "ClickLeftJoystick";
        public const string ResetCamera = "ClickRightJoystick";
        public const string EscapeMenu = "Escape";
        public const string SwitchCamToNextPlayer = "SwitchCamToNextPlayer";
    }

    // Assets
    public const string DiskPrefabName = "Prefab/Disk";
    public const string RocketPrefabName = "Prefabs/Rocket";
    public const string BlueKartTextureName = "Materials/KartBlueBody";
    public const string RedKartTextureName = "Materials/KartRedBody";
    public const string ClassicBattleEndMenu = "Menu/ClassicBattleEndMenu";

    // Scenes
    public class Scene
    {
        public const int Menu = 0;
        public const int GameHUD = 1;
        public const int FortBlock = 2;
        public const int Room = 3;
    }
}
