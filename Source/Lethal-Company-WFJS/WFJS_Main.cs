using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using NAudio.Wave;
using UnityEngine;

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

    public static AudioClip Jumpscare;

    public ManualLogSource Log;

    public WFJS_Main()
    {
        Instance = this;
    }
    
    private void Awake()
    {
        Log = Logger;

        var assembly = Assembly.GetExecutingAssembly();
        
        var harmony = new Harmony(PluginGuid);
        harmony.PatchAll(assembly);
        
        Inputs = new WFJS_Inputs();

        // Reads the .wav file and loads the samples into an array, which is then loaded in to an audio clip.
        // There might be a better way to do this. If there is, I don't really care, because this is good enough.
        // Audio file is stored in "assets" folder in same location as the assembly.
        var jumpscarePath = Path.Combine(assembly.Location, "..", "assets", "jumpscare.wav");
        //Log.LogDebug($"Jumpscare path: {jumpscarePath}");
        using var reader = new WaveFileReader(jumpscarePath);
        
        // Values are hardcoded because the audio file is too. It's stereo with 2 channels at 44.1 kHz.
        Jumpscare = AudioClip.Create("WFJS_Jumpscare", (int)reader.SampleCount, 2, 
            44100, false);

        // Load audio data to buffer as floats. I don't like how many 2-float arrays are created and immediately
        // discarded here, but it shouldn't make more than maybe a megabyte or two of trash so it should be fine.
        var buffer = new float[reader.SampleCount * 2];
        var index = 0;
        while (reader.ReadNextSampleFrame() is { } data)
        {
            buffer[index] = data[0];
            buffer[index + 1] = data[1];
            index += 2;
        }

        // Set the audio clip's data.
        Jumpscare.SetData(buffer, 0);
    }
}