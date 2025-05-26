using UnityEngine;

public class VFXPoolManager : GenericPoolManager<VisualFX>
{
    public static VFXPoolManager Instance;
    protected override void Awake()
    {
        Instance = this;
        hidePoolObjectOnInit = false;
        base.Awake();
    }
    public void PlayVFXAtPosition(string sfxName, Vector3 position)
    {
        var vfx = SpawnFromPool(sfxName);      // take the first prefab since its the only SFX prefab that we need 
        vfx.transform.position = position;
        vfx.PlayVFX();
    }
}

