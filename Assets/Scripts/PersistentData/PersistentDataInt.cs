using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataInt : PersistentDataBase<int>
{
    public PersistentDataInt(string currentKey) : base(currentKey)
    {
    }

    public override void deleteValue(string param = null)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
