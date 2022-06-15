using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace MuckMod
{
    [BepInPlugin("com.atomic.muck","Atomic's Muck Mod","0.0.1")]
    public class Main : BaseUnityPlugin
    {
        public static ConfigEntry<bool> modEnabled { get; set; }
        public void Awake()
        {
            registerConfig();
            Logger.LogMessage("Config Registered");
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        }
        private void registerConfig()
        {
            modEnabled = Config.Bind<bool>("Mod", "IsEnabled", true, "Do you want the mod to load or not.");
        }



        [HarmonyPatch(typeof(StructureSpawner),"FindObjectToSpawn")]
        public static class Thing
        {
            private static void Postfix(StructureSpawner.WeightedSpawn[] structurePrefabs, ref GameObject __result)
            {
                if (__result.name == "TotemRespawn" && GameManager.gameSettings.multiplayer == GameSettings.Multiplayer.Off) 
                {
                    __result = structurePrefabs[0].prefab;
                   
                }
            }

        }
    }

    
}