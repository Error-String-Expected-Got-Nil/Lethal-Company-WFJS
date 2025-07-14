using HarmonyLib;

namespace Lethal_Company_WFJS.Patches;

[HarmonyPatch(typeof(MenuManager))]
public static class Patch_MenuManager
{
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    public static void Postfix_Update()
    {
        if (!WFJS_Main.Inputs.Toggle.WasReleasedThisFrame()) return;
        WFJS_Main.Enabled = !WFJS_Main.Enabled;
        WFJS_Main.Instance.Log.LogInfo(WFJS_Main.Enabled ? "Functionality enabled." : "Functionality disabled.");
    }
}