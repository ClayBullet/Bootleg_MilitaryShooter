using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataFloat_LOCAL : PersistentDataFloat
{
    protected DataProviderPlayerPrefs localProvider;

    public PersistentDataFloat_LOCAL(string currentKey) : base(currentKey)
    {
        localProvider = GameManager.managerGame.systemDataPersistent.dataProviderPlayer;

    }

    protected override void Initialize()
    {
       
    }

    public override void deleteValue(string param = null)
    {
        localProvider.deleteKey(param);

    }

    public override float getValueByTheCache()
    {
        return cachedValue;
    }
    public override void SetValue(float newVal)
    {
        localProvider.setFloatValue(key, newVal);

    }

    
    public void AddAndSetValue(float newVal)
    {
        localProvider.setFloatValue(key, newVal + localProvider.getFloatValue(key));
    }

    public float getValue()
    {
        return localProvider.getFloatValue(key);
    }
}
