using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Lethal_Company_WFJS.Patches;

[HarmonyPatch(typeof(HUDManager))]
public static class Patch_HUDManager
{
    // TODO: Debug code, remove
    [HarmonyPatch("Awake")]
    [HarmonyPostfix]
    public static void Postfix_Awake()
    {
        WFJS_Main.Instance.Log.LogDebug("HUDManager awaking");
    }

    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    public static void Postfix_Update()
    {
        
    }
}