public class Teleport : MovementTargetPoint, ITeleport
{
    private MovementTargetPoint _destination;

    public MovementTargetPoint Destination => _destination;

    public void Init(MovementTargetPoint destination)
    {
        _destination = destination;
    }
}