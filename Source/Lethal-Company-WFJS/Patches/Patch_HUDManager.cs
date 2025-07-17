using HarmonyLib;

namespace Lethal_Company_WFJS.Patches;

[HarmonyPatch(typeof(HUDManager))]
public static class Patch_HUDManager
{
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    public static void Postfix_Update()
    {
        
    }
}