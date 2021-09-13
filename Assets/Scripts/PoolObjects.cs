using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class PoolObjects : MonoBehaviour
{
    public void CreatePool(GameObject gameObjectForInstantiate, int numberOfThisPool, string transformParentName, ref List<GameObject> poolGameObjects)
    {
        GameObject parent = new GameObject();
        GameObject go = Instantiate(parent);
        Destroy(parent);

        go.name = transformParentName;
        for (int i = 0; i < numberOfThisPool; i++)
        {
            GameObject instaObject = Instantiate(gameObjectForInstantiate);
            instaObject.transform.parent = go.transform;
            instaObject.SetActive(false);
            poolGameObjects.Add(instaObject);
        }
    }

    public GameObject UseTheGameObject(List<GameObject> poolGameObjects, Vector3 position, Quaternion quaternion)
    {
        foreach (GameObject pool in poolGameObjects)
        {
            if (!pool.activeSelf)
            {
                pool.transform.position = position;
                pool.transform.rotation = quaternion;
                return pool;
            }

        }

        return null;


    }

    public IEnumerator DelayForDisable(GameObject objectForDisable, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectForDisable.SetActive(false);
    }
}
