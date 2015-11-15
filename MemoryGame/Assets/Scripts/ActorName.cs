using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActorName : MonoBehaviour {

    private Text _text;

    void Awake() {
        _text = GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
	    _text.text = AvatarManager.Instance.GetRandomName();
	}
}
