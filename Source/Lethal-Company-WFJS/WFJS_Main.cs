using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Lethal_Company_WFJS;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class WFJS_Main : BaseUnityPlugin
{
    public const string PluginGuid = "esegn.wfjs";
    public const string PluginName = "WFJS";
    public const string PluginVersion = "1.0.0";

    internal static WFJS_Inputs Inputs;
    
    public static WFJS_Main Instance;
    public static bool Enabled = true;

    public ManualLogSource Log;

    public WFJS_Main()
    {
        Instance = this;
    }
    
    private void Awake()
    {
        Log = Logger;

        var harmony = new Harmony(PluginGuid);
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        
        Inputs = new WFJS_Inputs();
    }
}