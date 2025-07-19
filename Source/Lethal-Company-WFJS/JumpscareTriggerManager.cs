using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lethal_Company_WFJS;

public class JumpscareTriggerManager : MonoBehaviour
{
    // Chance of jumpscare occuring each second when conditions are met.
    private const float Chance = 1f / 10000f;

    private static JumpscareHandler _jumpscare;

    private void Awake()
    {
        StartCoroutine(RollCoroutine());
    }

    private void Update()
    {
        // If there's an active jumpscare, tick it. If it returns false (jumpscare is over), tell it to destroy itself,
        // then discard the reference.
        if (!_jumpscare?.Tick(Time.deltaTime) ?? false)
        {
            _jumpscare.Destroy();
            _jumpscare = null;
        }
        
#if DEBUG
        if (WFJS_Main.Inputs.Test.WasReleasedThisFrame())
        {
            _jumpscare ??= new JumpscareHandler(HUDManager.Instance.UIAudio);
        }
#endif
    }
    
    private static IEnumerator RollCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (CanRollForJumpscare() && Random.value <= Chance)
                _jumpscare = new JumpscareHandler(HUDManager.Instance.UIAudio);
        }
        // This loops forever intentionally.
        // ReSharper disable once IteratorNeverReturns
    }
    
    private static bool CanRollForJumpscare()
        => WFJS_Main.Enabled
           && _jumpscare == null
           && !GameNetworkManager.Instance.localPlayerController.isPlayerDead
           && StartOfRound.Instance.shipHasLanded
           && !StartOfRound.Instance.shipIsLeaving
           && StartOfRound.Instance.currentLevel.PlanetName != "71 Gordion";
}