using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public static class GamepadUtils
{
    const string    BUTTON =    "Button";
    const string    STICK  =    "Stick" ;
    public static Gamepad gamepad = Gamepad.current;

    public static ButtonControl[] _buttons = new ButtonControl[] {    
                                    gamepad.buttonSouth,
                                    gamepad.buttonWest,
                                    gamepad.buttonEast,
                                    gamepad.buttonNorth,
                                    gamepad.dpad.up,
                                    gamepad.dpad.down,
                                    gamepad.dpad.left,
                                    gamepad.dpad.right,
                                    gamepad.selectButton,
                                    gamepad.startButton,
                                    gamepad.leftShoulder,
                                    gamepad.leftStickButton,
                                    gamepad.leftTrigger,
                                    gamepad.rightShoulder,
                                    gamepad.rightStickButton,
                                    gamepad.rightTrigger
                                };
                                    
    public static StickControl[]  _sticks = new StickControl[]  {   
                                    gamepad.leftStick,
                                    gamepad.rightStick
                                };

    [SerializeField]
    public static readonly Dictionary<string, (string, ButtonControl, AxisControl)> actionBindings = new Dictionary<string, (string, ButtonControl, AxisControl)>{
        //    action    |  axisType  |  physical button
        {   "MainSelect", (  BUTTON , gamepad.buttonSouth   , null                  ) },
        {    "AltSelect", (  BUTTON , gamepad.buttonEast    , null                  ) },
        {           "Up", (  BUTTON , gamepad.dpad.up       , null                  ) },
        {         "Down", (  BUTTON , gamepad.dpad.down     , null                  ) },
        {        "Right", (  BUTTON , gamepad.dpad.right    , null                  ) },
        {         "Left", (  BUTTON , gamepad.dpad.left     , null                  ) },
        {   "Accelerate", (  BUTTON , gamepad.rightTrigger  , null                  ) },
        {         "Look", (   STICK , null                  , gamepad.leftStick.x   ) },

    };
    public static string gamepads = string.Join("\n", Gamepad.all);
    
    public static IEnumerable<ButtonControl> GamepadButtonsPressed(){
        return _buttons?.Where(button => button.isPressed);
    }

    public static string GamepadButtonsPressedNames(){
        return string.Join("\n", GamepadButtonsPressed().Select(x => x.displayName));
    }

    public static IEnumerable<StickControl> GamepadSticks(){
        return _sticks.Select(axis => axis);
    }

    public static float ButtonValue(string action){
        return (actionBindings[action].Item1 == BUTTON) 
                    ? actionBindings[action].Item2.IsPressed() ? 1f : 0f 
                    : actionBindings[action].Item1 == STICK
                        ? actionBindings[action].Item3.ReadValue()
                        : 0f;
    }
}
