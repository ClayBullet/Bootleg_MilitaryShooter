using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataIntArray_LOCAL : PersistentDataInt
{
    protected DataProviderPlayerPrefs localProvider;

    public PersistentDataIntArray_LOCAL(string currentKey) : base(currentKey)
    {
        localProvider = GameManager.managerGame.systemDataPersistent.dataProviderPlayer;

    }

    public int actualLengthArray(string key)
    {
        return localProvider.getIntValue(key);
    }

    public void SetValue(int newVal, int index)
    {
        localProvider.setIntValue(key + index, newVal);

    }


    public void AddAndSetValue(int newVal, int index)
    {
        localProvider.setIntValue(key + index, newVal + localProvider.getIntValue(key));
    }

    public float getValue(int index)
    {
        return localProvider.getIntValue(key + index);
    }
}
