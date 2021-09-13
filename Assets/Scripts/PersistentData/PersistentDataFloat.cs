using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataFloat : PersistentDataBase<float>
{
    
    protected override void Initialize()
    {
    }
    public PersistentDataFloat(string currentKey) : base(currentKey)
    {
    }

    public override void deleteValue(string param = null)
    {
        
    }

   
}
