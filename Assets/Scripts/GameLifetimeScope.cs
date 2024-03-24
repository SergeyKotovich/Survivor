using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private bl_Joystick _blJoystick;
    protected override void Configure(IContainerBuilder builder)
    {
        
        if (Application.isMobilePlatform)
        {
            _blJoystick.gameObject.SetActive(true);
            
            builder.RegisterInstance(_blJoystick);
            
            builder.Register<MobilInputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        else
        {
            builder.Register<StandaloneInputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}