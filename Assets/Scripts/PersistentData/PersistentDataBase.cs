using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistentDataBase<T>
{
    public string key;
    public T cachedValue;


    public PersistentDataBase(string currentKey)
    {
        key = currentKey;
    }

    protected abstract void Initialize();

    public virtual T getValueByTheCache()
    {
        return cachedValue;
    }
    public virtual void SetValue(T newVal)
    {
        cachedValue = newVal;
    }

    public abstract void deleteValue(string param = null);
}
