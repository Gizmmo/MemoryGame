using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AvatarManager : Manager<AvatarManager> {

    #region Public Variables
    [Tooltip("Sprites that can be used as headshots for different people")]
    public List<Sprite> Headshots;

    [Tooltip("Strings that are used as names for the different people")]
    public List<string> Names;

    public List<ActorAvatar> Avatars;

    public Text Banner;
    public Button ActionButton;
    private Text _buttonText;

    private List<AvatarPair> _storedAvatarPairs = new List<AvatarPair>();

    private enum GameState {
        Start,
        Memorize,
        Countdown,
        Choose,
        Result,
        Restart
    }

    private GameState _currentGameState = GameState.Start;
    #endregion

    void Start() {
        _buttonText = ActionButton.GetComponentInChildren<Text>();
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

    public void ActionClick() {
        switch (_currentGameState) {

            case GameState.Start:
                EnableAllAvatars();
                _currentGameState = GameState.Memorize;
                SetNewAvatars();
                Banner.text = "Memorize!";
                _buttonText.text = "Go!";
                break;

            case GameState.Memorize:
                _currentGameState = GameState.Restart;
                RandomizeAllAvatars();
                Banner.text = "Choose the Correct Pairs!";
                _buttonText.text = "Restart";
                break;

            case GameState.Restart:
                _currentGameState = GameState.Start;
                DisableAllAvatars();
                Banner.text = "Click Start To Begin";
                _buttonText.text = "Start";
                break;
            case GameState.Countdown:
                break;
            case GameState.Choose:
                break;
            case GameState.Result:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// Enables all Avatars on Screen
    /// </summary>
    public void EnableAllAvatars() {
        SetAvatarsEnabled(true);
    }

    /// <summary>
    /// Disables All Avatars on Screen
    /// </summary>
    public void DisableAllAvatars() {
        SetAvatarsEnabled(false);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets all Avatars enabled attribute to the boolean passed
    /// </summary>
    /// <param name="isEnabled">Enables all Avatars if true, false if not</param>
    private void SetAvatarsEnabled(bool isEnabled) {
        
        // For each Avatar in the Avatar array...
        for (var i = 0; i < Avatars.Count; i++) {

            // ..change the enable attribute to the bool passed
            Avatars[i].gameObject.SetActive(isEnabled);
        }
    }

    /// <summary>
    /// Sets the list avatars to each a new random AvatarPair
    /// </summary>
    private void SetNewAvatars() {
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
    private Sprite GetRandomSprite() {
        return GetRandomListElement(Headshots);
    }

    /// <summary>
    /// Returns a random string from the Names list
    /// </summary>
    /// <returns>A String name</returns>
    private string GetRandomName() {
        return GetRandomListElement(Names);
    }

    /// <summary>
    /// Returns a random element from the passed List, and removes it from the list
    /// </summary>
    /// <typeparam name="T">The type of element returned</typeparam>
    /// <param name="list">A list of T elements with one being returned</param>
    /// <param name="removeElement">if true, the element will be removed from the list once randomly chosen</param>
    /// <returns>An element of T type</returns>
    private static T GetRandomListElement<T>(IList<T> list, bool removeElement = false) {
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
