using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add to asset menu
[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public int index;
    public int amount;
    public GameObject bullet;
}
