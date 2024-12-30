namespace GameStateManagementSample.Models.Entities.States;

public interface IEnemyState
{
    public void Execute();
    public void Enter();
    public void Exit();

}