using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AvatarActor : MonoBehaviour {

    private Image _avatarImage;

    void Awake() {
        _avatarImage = GetComponent<Image>();
    }

	// Use this for initialization
	void Start () {
	    _avatarImage.sprite = AvatarManager.Instance.GetRandomSprite();
	}
}
