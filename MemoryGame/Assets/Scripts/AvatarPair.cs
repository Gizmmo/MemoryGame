using UnityEngine;

public class AvatarPair {

    public string Name; //The name of the Avatar
    public Sprite HeadShot;  //The headshot for the avatar

    /// <summary>
    /// Constructor for the Avatar pair class
    /// </summary>
    /// <param name="headShot">The new headashot sprite for this avatar pair</param>
    /// <param name="name">The new name for this avatar pair</param>
    public AvatarPair(Sprite headShot, string name) {
        Name = name;
        HeadShot = headShot;
    }
}
