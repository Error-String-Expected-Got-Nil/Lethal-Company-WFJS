using HarmonyLib;
// ReSharper disable InconsistentNaming

namespace Lethal_Company_WFJS.Patches;

[HarmonyPatch(typeof(MenuManager))]
public static class Patch_MenuManager
{
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    public static void Postfix_Update(MenuManager __instance)
    {
        if (WFJS_Main.Inputs.Toggle.WasReleasedThisFrame())
        {
            WFJS_Main.Enabled = !WFJS_Main.Enabled;
            WFJS_Main.Instance.Log.LogInfo(WFJS_Main.Enabled ? "Functionality enabled." : "Functionality disabled.");
        }

#if DEBUG
        if (WFJS_Main.Inputs.Test.WasReleasedThisFrame())
        {
            __instance.MenuAudio.PlayOneShot(WFJS_Main.Jumpscare);
        }
#endif
    }
}