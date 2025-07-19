using HarmonyLib;
using UnityEngine;

namespace Lethal_Company_WFJS.Patches;

[HarmonyPatch(typeof(HUDManager))]
public static class Patch_HUDManager
{
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    public static void Postfix_Update()
    {
#if DEBUG
        if (WFJS_Main.Inputs.Test.WasReleasedThisFrame())
        {
            foreach(var source in Object.FindObjectsOfType<AudioSource>())
                WFJS_Main.Instance.Log.LogDebug(source);
        }
#endif
    }
}