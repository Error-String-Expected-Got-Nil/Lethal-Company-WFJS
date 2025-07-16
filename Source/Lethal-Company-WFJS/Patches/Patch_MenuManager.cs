using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

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
            var canvasObj = new GameObject { name = "Test" };
            //canvasObj.AddComponent<CanvasScaler>();
            var canvas = canvasObj.AddComponent<Canvas>();

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var imageObj = new GameObject
            {
                name = "Image",
                transform = { parent = canvasObj.transform }
            };

            var img = imageObj.AddComponent<RawImage>();
            img.texture = WFJS_Main.TestTexture;
        }
#endif
    }
}