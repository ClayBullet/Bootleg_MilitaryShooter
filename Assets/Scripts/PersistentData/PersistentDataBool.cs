using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataBool : PersistentDataBase<bool>
{
    protected override void Initialize()
    {
        
    }
    public PersistentDataBool(string currentKey) : base(currentKey)
    {
    }

    public override void deleteValue(string param = null)
    {
        
    }

   
}
