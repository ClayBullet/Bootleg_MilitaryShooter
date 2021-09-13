using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataProviderBase 
{
    public virtual bool HasKey(string key)
    {

        return false;
    }

    public abstract int getIntValue(string key);

    public abstract string getStringValue(string key);

    public abstract bool getBoolValue(string key);

    public abstract float getFloatValue(string key);

    public abstract void setIntValue(string key, int newVal);

    public abstract void setStringValue(string key, string newVal);

    public abstract void setBoolValue(string key, bool newVal);

    public abstract void setFloatValue(string key, float newVal);

    public abstract void clearAll();

    public abstract void deleteKey(string key);
}
