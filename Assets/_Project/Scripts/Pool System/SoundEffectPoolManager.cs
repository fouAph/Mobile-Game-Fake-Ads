using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundEffectPoolManager : GenericPoolManager<AudioFX>
{
    public static SoundEffectPoolManager Instance;
    public SFX[] SFXList;
    public Dictionary<string, AudioClip[]> SFXDictionary = new();
    protected override void Awake()
    {
        Instance = this;
        hidePoolObjectOnInit = false;
        base.Awake();
    }

    private void Start()
    {
        foreach (var item in SFXList)
        {
            if (item.sfxName == "")
            {
                item.sfxName = item.sfxClip[0].name;
            }
            SFXDictionary.Add(item.sfxName, item.sfxClip);
        }
    }

    public void PlayAudioToPosition(string sfxName, Vector3 position)
    {
        var sfx = SpawnFromPool(poolItems[0].prefab.name);      // take the first prefab since its the only SFX prefab that we need 
        sfx.transform.position = position;
        sfx.PlaySFX(SFXDictionary[sfxName]);
    }

    [Serializable]
    public class SFX
    {
        public string sfxName;
        public AudioClip[] sfxClip;
    }
}

