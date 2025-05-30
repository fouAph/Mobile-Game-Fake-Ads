using UnityEngine;

public abstract class GameModeBase : MonoBehaviour, IGameMode
{
    public bool isGameStarted;
    public abstract void EndGame();

    public abstract void GameUpdate();

    public abstract void OnEnemyDie();

    public abstract void StartGame();
}
