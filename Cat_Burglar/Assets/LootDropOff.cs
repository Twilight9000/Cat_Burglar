using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropOff : MonoBehaviour
{
    private Vector3 lootSpawnPoint;
    private GameController gc;
    private void Awake()
    {
        gc = GameObject.FindObjectOfType<GameController>();
        lootSpawnPoint = GameObject.Find("LootDropOffCube").transform.GetChild(6).transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.TryGetComponent<CatBehaviour>(out CatBehaviour cat);
            
            foreach (GameObject g in cat.objectsStolen)
            {
                cat.currentCarriedWeight -= g.GetComponent<LootScript>().weight;
                g.transform.position = lootSpawnPoint;
                g.gameObject.SetActive(true);
            }
            cat.objectsStolen.Clear();

            gc.totalMoneyScore += gc.moneyCaried;
            gc.moneyCaried = 0;
            gc.UpdateText();
            
        }
    }
}
