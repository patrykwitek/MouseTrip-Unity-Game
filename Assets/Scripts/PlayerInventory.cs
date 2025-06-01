using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<int> collectedKeys = new List<int>();

    public void AddKey(int keyID)
    {
        if (!collectedKeys.Contains(keyID))
        {
            collectedKeys.Add(keyID);
        }
    }

    public bool HasKey(int keyID)
    {
        return collectedKeys.Contains(keyID);
    }
}