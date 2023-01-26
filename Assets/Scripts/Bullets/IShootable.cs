using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable {
    
    
    void OnUpdate (Vector2 direction);
    void OnHit ();
} 

