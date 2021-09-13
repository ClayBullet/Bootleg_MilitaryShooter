using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataInt_LOCAL : PersistentDataInt
{
    protected DataProviderPlayerPrefs localProvider;

    public PersistentDataInt_LOCAL(string currentKey) : base(currentKey)
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

    public override int getValueByTheCache()
    {
        return cachedValue;
    }
    public override void SetValue(int newVal)
    {
        localProvider.setIntValue(key, newVal);

    }

    public int getValue()
    {
        return localProvider.getIntValue(key);
    }

    public void AddAndSetValue(int newVal)
    {
        localProvider.setIntValue(key, newVal + localProvider.getIntValue(key));
    }

}
