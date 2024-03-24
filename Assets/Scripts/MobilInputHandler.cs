using UnityEngine;
using VContainer;

public class MobilInputHandler : IInputHandler
{
    private bl_Joystick _joystick;
    
    [Inject]
    public void Construct(bl_Joystick joystick)
    {
        _joystick = joystick;
    }
    public Vector2 GetInput()
    {
        return new Vector2(_joystick.Horizontal, _joystick.Vertical);
    }
}