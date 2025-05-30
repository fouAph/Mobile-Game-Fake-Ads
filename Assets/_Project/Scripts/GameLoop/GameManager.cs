using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameModeType gameModeType;
    private int credit;
    [SerializeField] GameModeBase[] GameModePrefabs;
    [SerializeField] BoxCollider spawnCollider;

    private IGameMode currentGameMode;

    protected override void Awake()
    {
        base.Awake();
        currentGameMode = CreateGameMode(gameModeType);
    }

    private void Start()
    {
        currentGameMode.StartGame();
    }

    void Update()
    {
        currentGameMode.GameUpdate();
    }

    public void OnEnemyDie()
    {
        currentGameMode.OnEnemyDie(); 
    }

    private IGameMode CreateGameMode(GameModeType type)
    {
        switch (type)
        {
            case GameModeType.Wave:
                return FindAndInstantiateGameMode<WaveMode>();

            // Add cases for other game modes
            default:
                Debug.LogError("Unsupported Game Mode");
                return null;
        }

        IGameMode FindAndInstantiateGameMode<T>() where T : GameModeBase
        {
            foreach (var prefab in GameModePrefabs)
            {
                if (prefab is T)
                {
                    var instance = Instantiate(prefab, transform);
                    if (instance.TryGetComponent<IGameMode>(out var gameMode))
                        return gameMode;

                    Debug.LogError($"Prefab of type {typeof(T)} does not implement IGameMode.");
                    return null;
                }
            }

            Debug.LogError($"Game Mode of type {typeof(T)} not found in prefabs.");
            return null;
        }
    }

    public void SpawnEnemy(string enemyName, Vector3 pos = new())
    {
        EnemyPoolSystem.Instance.SpawnEnemyAtPosition("Zombie", pos == Vector3.zero ? GetRandomPositionFromBoxCollider() : pos);

    }

    public Vector3 GetRandomPositionFromBoxCollider()
    {
        Vector3 pos = new(Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x), spawnCollider.bounds.min.y, Random.Range(spawnCollider.bounds.min.z, spawnCollider.bounds.max.z));

        return pos;
    }

    public void AddCredit(int value) => credit += value;

    public void SubstractCredit(int value) => credit -= value;

    public bool CheckIfCreditEnough(int itemValue) => credit >= itemValue;


    public enum GameModeType { Wave, Survival, TimeAttack }
}
