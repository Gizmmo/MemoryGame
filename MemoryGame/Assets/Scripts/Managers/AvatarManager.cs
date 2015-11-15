using UnityEngine;
using System.Collections.Generic;

public class AvatarManager : Manager<AvatarManager> {

    #region Public Variables
    [Tooltip("Sprites that can be used as headshots for different people")]
    public List<Sprite> Headshots;
    [Tooltip("Strings that are used as names for the different people")]
    public List<string> Names;
    #endregion

    #region Public Methods
    /// <summary>
    /// Returns a random Sprite from the Headshots list
    /// </summary>
    /// <returns>A Sprite headshot</returns>
    public Sprite GetRandomSprite() {
        return GetRandomListElement(Headshots);
    }

    /// <summary>
    /// Returns a random string from the Names list
    /// </summary>
    /// <returns>A String name</returns>
    public string GetRandomName() {
        return GetRandomListElement(Names);
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Returns a random element from the passed List, and removes it from the list
    /// </summary>
    /// <typeparam name="T">The type of element returned</typeparam>
    /// <param name="list">A list of T elements with one being returned</param>
    /// <returns>An element of T type</returns>
    private static T GetRandomListElement<T>(IList<T> list) {
        
        //Get a random number between 0 and the total list count
        var avatarPosition = Random.Range(0, list.Count);

        //Get the element at the random position
        var returnElement = list[avatarPosition];

        //Remove that element from the list
        list.RemoveAt(avatarPosition);

        //Return the removed element
        return returnElement;
    }
    #endregion
}
