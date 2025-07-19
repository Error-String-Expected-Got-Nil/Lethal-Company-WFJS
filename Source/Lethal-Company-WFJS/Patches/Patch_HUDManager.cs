using HarmonyLib;
using UnityEngine;

namespace Lethal_Company_WFJS.Patches;

[HarmonyPatch(typeof(HUDManager))]
public static class Patch_HUDManager
{
    private static GameObject _jumpscareTriggerContainer;

    [HarmonyPatch("OnEnable")]
    [HarmonyPostfix]
    public static void Postfix_OnEnable()
    {
        _jumpscareTriggerContainer = new GameObject { name = "JumpscareTriggerContainer" };
        _jumpscareTriggerContainer.AddComponent<JumpscareTriggerManager>();
    }

    [HarmonyPatch("OnDisable")]
    [HarmonyPostfix]
    public static void Postfix_OnDisable()
    {
        Object.Destroy(_jumpscareTriggerContainer);
    }
}