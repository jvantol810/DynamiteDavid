using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    //Speed: how fast the bullet will travel
    float speed { get; set; }
    //Duration: how long the bullet will last before clean-up -- if set to 0, the bullet will last forever
    float duration { get; set; }
    //Scale: the scale of the bullet's Transform component (z = 0)
    //Vector2 scale { get; set; }
    //Angle: the angle, in degrees, the bullet will be sent on fired.
    int angle { get; set; }
    //BulletSprite: The sprite that the bullet will look like
    Sprite bulletSprite { get; set; }
    //FireImmediately: if true, the bullet will be fired when created, if false then it will do nothing until fired manually.
    bool fireImmediately { get; set; }

    //Fired: set to false initially, and true when bullet is fired.
    //bool fired { get; }
    //TimeLeft: Will be set equal to duration on start, and will begin to decrease when the bullet is fired.
    //float timeLeft { get; }

    void InitializeInterface(float spd, float dur, int ang, Sprite bsprite, bool fireNow);
    void InitializeInterface(float spd, float dur, int ang);
}

public interface IBulletPierce
{
    //CanPierce: if true, clean-up will not occur on hit, if false clean-up will occur on the first hit.
    bool canPierce { get; set; }
}
