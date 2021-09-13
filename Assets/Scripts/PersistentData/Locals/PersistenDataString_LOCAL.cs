using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenDataString_LOCAL : PersistentDataString
{
    protected DataProviderPlayerPrefs localProvider;

    public PersistenDataString_LOCAL(string currentKey) : base(currentKey)
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

    public override string getValueByTheCache()
    {
        return cachedValue;
    }
    public override void SetValue(string newVal)
    {
        localProvider.setStringValue(key, newVal);

    }

    public void AddAndSetValue(string newVal)
    {
        localProvider.setStringValue(key, localProvider.getStringValue(key) + newVal);
    }

    public string getValue()
    {
        return localProvider.getStringValue(key);
    }
}
