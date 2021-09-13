using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = " New PopUpGroupAsset")]
    public class PopUpGroupAsset : ScriptableObject
    {
        public List<PopUpGroupData> prefab = new List<PopUpGroupData>();
        public bool exclusive = false;
    }
    public enum PopUpBlock
    {
        ENABLED = 0, DISABLED = 1
    }

    [System.Serializable]
    public class PopUpGroupData
    {
        public PopUpLayer popupPrefab;
        public PopUpBlock below;
    }


