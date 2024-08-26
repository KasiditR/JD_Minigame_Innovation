using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Boss.Billboard
{
    public enum CoreValue
    {
        Innovation,
        TeamSpirit,
        CustomerFirst,
        OwnerShip,
    }
    [System.Serializable]
    public class Text
    {
        [TextArea] public string detail = null;
    }
    [CreateAssetMenu(fileName = "BillboardItems", menuName = "Create ItemManager", order = 1)]
    public class BillboardItems : ScriptableObject
    {
        public CoreValue coreValue = CoreValue.Innovation;
        public GameObject billboardObj = null;
        public float duration = 5;
        
        [HorizontalLine]
        [Note("Index [0] = Eng , Index [1] = Thai")]
        public List<Text> textDetails = null;

        [HorizontalLine]
        public Vector3[] targets;
    }
}
