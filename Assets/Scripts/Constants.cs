﻿public class Constants
{
    // Layers
    public class Layer
    {
        public const string Ground = "Ground";
        public const string Kart = "Kart";
    }

    // Tags
    public class Tag
    {
        public const string RedSpawn = "RedSpawn";
        public const string BlueSpawn = "BlueSpawn";
        public const string Kart = "Kart";
        public const string KartHealthHitBox = "Health";
        public const string ItemCollisionHitBox = "CollisionHitBox";
        public const string ItemBox = "ItemBox";
        public const string IonBeamCamIgnore = "IonBeamCamIgnore";
        public const string Totem = "Totem";
        public const string TotemPickup = "TotemPickup";
        public const string TotemRespawn = "TotemRespawn";
    }

    // Inputs
    public class Input
    {
        public const string Accelerate = "Accelerate";
        public const string Decelerate = "Decelerate";
        public const string TurnAxis = "Turn";
        public const string UpAndDownAxis = "UpAndDown";
        public const string Drift = "Drift";
        public const string UseItem = "Item";
        public const string UseItemForward = "ItemForward";
        public const string UseItemBackward = "ItemBackward";
        public const string UseAbility = "Ability";
        public const string BackCamera = "ClickLeftJoystick";
        public const string TurnCamera = "RightJoystickHorizontal";
        public const string ResetCamera = "ClickRightJoystick";
        public const string EscapeMenu = "Escape";
        public const string SwitchCamToNextPlayer = "SwitchCamToNextPlayer";
    }

    // Scenes
    public class Scene
    {
        public const string MainMenu = "Menu";
    }

    // Prefabs
    public class Resources
    {
        public const string EndGameMenu = "Menu/EndGameMenu";
        public const string PlayerSettings = "PlayerSettings";
    }

    // GameObjects names
    public class GameObjectName
    {
        public const string Speedmeter = "Speedmeter";
    }

    // Materials
    public class Materials
    {
        public const string BlueKart = "Materials/KartBlueBody";
        public const string RedKart = "Materials/KartRedBody";
        public const string WhiteKart = "Materials/KartWhiteBody";
    }

    // GameModes
    public class GameModes
    {
        public const string Battle = "Battle";
        public const string Totem = "Totem";
    }
}
