using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] GameObject[] _lootItems;

    public void DropLoot()
    {
        int randomIndex = Random.Range(0, _lootItems.Length);
        GameObject selectedItem = _lootItems[randomIndex];

        Instantiate(selectedItem, transform.position, Quaternion.identity);
    }
}
