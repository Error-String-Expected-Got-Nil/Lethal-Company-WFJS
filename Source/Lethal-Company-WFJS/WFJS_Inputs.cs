using LethalCompanyInputUtils.Api;
using LethalCompanyInputUtils.BindingPathEnums;
using UnityEngine.InputSystem;

namespace Lethal_Company_WFJS;

public class WFJS_Inputs : LcInputActions
{
    [InputAction(KeyboardControl.Backslash, Name = "Toggle WFJS")]
    public InputAction Toggle { get; set; }
}