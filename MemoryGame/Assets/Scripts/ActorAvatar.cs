using System;
using UnityEngine;
using UnityEngine.UI;

public class ActorAvatar : MonoBehaviour {

    public Image AvatarImage;
    public Text Text;

	// Use this for initialization
	void Start () {
	    RandomizeAvatar();
    }

    /// <summary>
    /// Randomize the name and sprite for this avatar.
    /// </summary>
    void RandomizeAvatar() {
        AvatarImage.sprite = AvatarManager.Instance.GetRandomSprite();
        Text.text = AvatarManager.Instance.GetRandomName();
    }


}
