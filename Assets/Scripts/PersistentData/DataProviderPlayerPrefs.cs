using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProviderPlayerPrefs : DataProviderBase
{
    public override void clearAll()
    {
        throw new System.NotImplementedException();
    }

    public override void deleteKey(string key)
    {
         PlayerPrefs.DeleteKey(key);
    }

    public override bool getBoolValue(string key)
    {
        return false;
    }

    public override float getFloatValue(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public override int getIntValue(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public override string getStringValue(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public override void setBoolValue(string key, bool newVal)
    {
        throw new System.NotImplementedException();
    }

    public override void setFloatValue(string key, float newVal)
    {
        PlayerPrefs.SetFloat(key, newVal);
    }

    public override void setIntValue(string key, int newVal)
    {
        PlayerPrefs.SetInt(key, newVal);
    }

    public override void setStringValue(string key, string newVal)
    {
        PlayerPrefs.SetString(key, newVal);
    }

   
}
   

