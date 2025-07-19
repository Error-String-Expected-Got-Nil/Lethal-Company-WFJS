using HarmonyLib;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Lethal_Company_WFJS.Patches;

[HarmonyPatch(typeof(MenuManager))]
public static class Patch_MenuManager
{
#if DEBUG
    private static JumpscareHandler _testJumpscare;    
#endif
    
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
        if (_testJumpscare != null)
        {
            var finished = !_testJumpscare.Tick(Time.deltaTime);
            if (finished)
            {
                _testJumpscare.Destroy();
                _testJumpscare = null;
            }
        }
        
        if (WFJS_Main.Inputs.Test.WasReleasedThisFrame() && _testJumpscare == null)
        {
            _testJumpscare = new JumpscareHandler(__instance.MenuAudio);
        }
#endif
    }
}