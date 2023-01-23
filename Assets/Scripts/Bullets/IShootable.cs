using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable {
    int amount { get; set;}
    void OnUpdate ();
    void OnHit ();
} 

