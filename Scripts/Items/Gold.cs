using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Gold")]
public class Gold : Equipment
{
    public int value = 0;
    
    void OnEnable() {
        value = Random.Range(10, 50);
    }

}