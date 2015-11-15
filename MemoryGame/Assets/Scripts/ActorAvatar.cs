using System;
using UnityEngine;
using UnityEngine.UI;

public class ActorAvatar : MonoBehaviour {

    #region Public Variables
    [Tooltip("The Image UI component that will contain the headshot of the avatar to appear on screen")]
    public Image AvatarImage;

    [Tooltip("The Text UI component that will contain the name of the avatar to appear on screen")]
    public Text Text;
    #endregion

    #region Events
    public Action<ActorAvatar> AvatarOnChange; // Event that triggers when the name or headshot of the avatar changes
    #endregion

    void Start() {
        RandomizeAvatar();
    }

    #region Private Methods
    /// <summary>
    /// Randomize the name and sprite for this avatar.
    /// </summary>
    void RandomizeAvatar() {
       // Sets the avatar to a random sprite and name
       SetAvatar(AvatarManager.Instance.GetRandomSprite(), AvatarManager.Instance.GetRandomName());
    }

    /// <summary>
    /// Sets the avatar to the name and sprite passed
    /// </summary>
    /// <param name="sprite">The new headshot for the avatar</param>
    /// <param name="avatarName">The new name for the avatar</param>
    void SetAvatar(Sprite sprite, string avatarName) {

        // Set the sprite and text of the Avatar
        AvatarImage.sprite = sprite;
        Text.text = avatarName;
        
        // Trigger the AvatarOnChange event
        TriggerAvatarOnChange();
    }

    void TriggerAvatarOnChange() {
        AvatarOnChange.Run(this);
    }

    #endregion
}
