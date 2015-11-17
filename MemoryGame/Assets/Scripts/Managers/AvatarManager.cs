using UnityEngine;
using System.Collections.Generic;

public class AvatarManager : Manager<AvatarManager> {

    #region Public Variables
    [Tooltip("Sprites that can be used as headshots for different people")]
    public List<Sprite> Headshots;

    [Tooltip("Strings that are used as names for the different people")]
    public List<string> Names;

    public List<ActorAvatar> Avatars; 

    private List<AvatarPair> _storedAvatarPairs = new List<AvatarPair>();
    #endregion

    //Runs at the start
    void Start() {
        SetNewAvatars();
    }

    #region Public Methods
   
    /// <summary>
    /// Randomizes the headshot and names for all ActorAvatars in the Avatars array.
    /// </summary>
    public void RandomizeAllAvatars() {
        
        // For each ActorAvatar in the Avatars array...
        for (var i = 0; i < Avatars.Count; i++) {
            
            // ...Randomize the headshot and name.
            RandomizeAvatar(Avatars[i]);
        }
    }

    /// <summary>
    /// Sets the given Avatar to a random headshot and name
    /// </summary>
    /// <param name="avatar">The avatar to change the name and headshot of</param>
    public void RandomizeAvatar(ActorAvatar avatar) {
        avatar.SetAvatar(GetRandomSprite(), GetRandomName());
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets the list avatars to each a new random AvatarPair
    /// </summary>
    void SetNewAvatars() {
        
        // Clear out other stored Avatar Pairs
        _storedAvatarPairs.Clear();

        // For each Avatar Actor Component in the List...
        for (var i = 0; i < Avatars.Count; i++) {

            // ... Get a new sprtie and name...
            var sprite = GetRandomSprite();
            var avatarName = GetRandomName();

            // ... and store the two random variables in a new AvatarPair...
            _storedAvatarPairs.Add(new AvatarPair(sprite, avatarName));

            // ...and set the avatar on screen to that respective pair.
            Avatars[i].SetAvatar(sprite, avatarName);

        }

    }

    /// <summary>
    /// Returns a random Sprite from the Headshots list
    /// </summary>
    /// <returns>A Sprite headshot</returns>
    Sprite GetRandomSprite() {
        return GetRandomListElement(Headshots);
    }

    /// <summary>
    /// Returns a random string from the Names list
    /// </summary>
    /// <returns>A String name</returns>
    string GetRandomName() {
        return GetRandomListElement(Names);
    }

    /// <summary>
    /// Returns a random element from the passed List, and removes it from the list
    /// </summary>
    /// <typeparam name="T">The type of element returned</typeparam>
    /// <param name="list">A list of T elements with one being returned</param>
    /// <param name="removeElement">if true, the element will be removed from the list once randomly chosen</param>
    /// <returns>An element of T type</returns>
    static T GetRandomListElement<T>(IList<T> list, bool removeElement = false) {
        
        //Get a random number between 0 and the total list count
        var avatarPosition = Random.Range(0, list.Count);

        //Get the element at the random position
        var returnElement = list[avatarPosition];

        // If the optional parameter removeElement is set to true...
        if (removeElement) {
            // ...then remove that element from the list.
            list.RemoveAt(avatarPosition);
        }

        //Return the removed element
        return returnElement;
    }
    #endregion
}
