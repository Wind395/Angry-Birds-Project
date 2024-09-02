using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckBird : Bird
{

    void FixedUpdate()
    {
        
    }

    public override void LaunchBird (Vector2 direction, float force) 
    {
        base.LaunchBird(direction, force);
    }

    public override void SpecialAbility(Vector2 direction, float force) 
    {
        rb2d.velocity = direction * force * 3;

    }
}
