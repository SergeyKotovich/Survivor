using Cysharp.Threading.Tasks;

public interface IState: IInitializable
{
     void OnEnter();

}