using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : Hardware
{
    private readonly List<KeyCode> buttonKeycodes = new List<KeyCode> 
    { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
      KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    private readonly List<KeyCode> joystickKeycodes = new List<KeyCode>
    { KeyCode.JoystickButton0, KeyCode.JoystickButton1, KeyCode.JoystickButton2, 
      KeyCode.JoystickButton3, KeyCode.JoystickButton4, KeyCode.JoystickButton5, 
      KeyCode.JoystickButton6, KeyCode.JoystickButton7, KeyCode.JoystickButton8, 
      KeyCode.JoystickButton9 };

    public Joystick()
    {
        type = "Joystick";
        booleans = new bool[] { false, false, false, false, false, false, false, false, false, false };
        integers = new int[] { 0, 0 };
        doubles = new double[] { 0.0, 0.0, 0.0, 0.0 };
        strings = new string[0];
    }

    public Joystick(int joystickID)
    {
        type = "Joystick";
        booleans = new bool[] {false, false, false, false, false, false, false, false, false, false};
        integers = new int[] { joystickID, 0 };
        doubles = new double[] { 0.0, 0.0, 0.0, 0.0 };
        strings = new string[0];
    }

    public override void CopyRelValues(Hardware other)
    {
        return;
    }

    public void Loop()
    {
        doubles[0] = Input.GetAxis("Horizontal");
        doubles[1] = Input.GetAxis("Vertical");
        doubles[2] = Input.GetAxis("Horizontal2");
        doubles[3] = Input.GetAxis("Vertical2");

        for (int i = 0; i < booleans.Length; i++)
        {
            booleans[i] = Input.GetKey(buttonKeycodes[i]) || Input.GetKey(joystickKeycodes[i]);
        }
    }
}
