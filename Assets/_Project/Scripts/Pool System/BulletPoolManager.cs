using UnityEngine;

public class BulletPoolManager : GenericPoolManager<Bullet>
{
    public static BulletPoolManager Instance;

    protected override void Awake()
    {
        Instance = this;
        base.Awake();
    }
}
