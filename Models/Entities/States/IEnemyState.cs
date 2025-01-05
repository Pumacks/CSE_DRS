namespace GameStateManagementSample.Models.Entities.States;

public interface IEnemyState
{
    public void Execute(Enemy enemy);
    public void Enter(Enemy enemy);
    public void Exit(Enemy enemy);

}