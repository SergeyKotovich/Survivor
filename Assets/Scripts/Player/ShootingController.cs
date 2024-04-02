using VContainer;

public class ShootingController
{
    private IWeapon _weapon;
    [Inject] 
    public void Construct( IWeapon weapon)
    {
        _weapon = weapon;
    }
}