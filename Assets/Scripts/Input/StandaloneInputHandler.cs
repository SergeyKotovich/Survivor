using UnityEngine;

public class StandaloneInputHandler : IInputHandler
{
    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}