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
    void RandomizeAllAvatars() {
        var randomizedNameOrder = GetRandomIntArray(_storedAvatarPairs.Count);
        var randomizedSpirteOrder = GetRandomIntArray(_storedAvatarPairs.Count);

        // For each ActorAvatar in the Avatars array...
        for (var i = 0; i < Avatars.Count; i++) {
            
            // ...Randomize the headshot and name.
            Avatars[i].SetAvatar(_storedAvatarPairs[randomizedNameOrder[i]].HeadShot, _storedAvatarPairs[randomizedSpirteOrder[i]].Name);
        }
    }

    /// <summary>
    /// Returns a random int array between 0 and the size passed.
    /// </summary>
    /// <returns>A int array with random numbers as elements</returns>
    int[] GetRandomIntArray(int size) {
        return GetRandomIntArray(size, size);
    }

    /// <summary>
    /// Returns a random int array between 0 and the max passed.
    /// </summary>
    /// <returns>A int array with random numbers as elements</returns>
    int[] GetRandomIntArray(int size, int max) {
        
        // Create a new int array of the max amount passed
        var fullArray = new int[max];
        // Create a new int array of the size passed
        var returnArray = new int[size];

        // For each element in the int array...
        for (var i = 0; i < max; i++) {
            // ...put the number of the index in the array position
            fullArray[i] = i;
        }

        // Shuffle the fullArray.
        ShuffleArray(fullArray);

        // For each element in the returnArray...
        for (var i = 0; i < size; i++) {
            // ...grab the corresponding index of the randomized array.
            returnArray[i] = fullArray[i];
        }

        // Return the array Shuffled with only the passed size.
        return returnArray;
    }

    /// <summary>
    /// Shuffles an array and returns it
    /// </summary>
    /// <typeparam name="T">The type of array to shuffle and return</typeparam>
    /// <param name="arr">The array to randomize</param>
    /// <returns>The randomized arrays</returns>
    public static T[] ShuffleArray<T>(T[] arr) {
        
        // For each element in the passed array...
        for (var i = arr.Length - 1; i > 0; i--) {
            
            // ...then Get a random number between 0 and the current index...
            var r = Random.Range(0, i);

            // ...and store the i index array element in a temporary variable...
            T tmp = arr[i];

            // ...and store the random element into the index position element...
            arr[i] = arr[r];

            // ...and then store the temp variable back into the random position element.
            arr[r] = tmp;
        }

        //Return the shuffeled array
        return arr;
    }

    /// <summary>
    /// Called whenever the Action Button is clicked
    /// </summary>
    public void ActionClick() {
        
        // Depending on the State in the Current Game State...
        switch (_currentGameState) {

            // ...if the state is on Start...
            case GameState.Start:
                
                // ...then Enable all disabled Avatars...
                EnableAllAvatars();

                // ...and Change the state to Memorize...
                _currentGameState = GameState.Memorize;

                // ...and Randomize and Save the new Avatar headshots and names...
                SetNewAvatars();

                // ...and update the text in the Button and Banner.
                Banner.text = "Memorize!";
                _buttonText.text = "Go!";

                break;

            // ...if the state is on Memorize...
            case GameState.Memorize:
                
                // ...then change the gameState to Restart...
                _currentGameState = GameState.Restart;

                // ...and Randomize all avatars (This will need to be updated)...
                RandomizeAllAvatars();

                // ...and Update the banner and button Text.
                Banner.text = "Choose the Correct Pairs!";
                _buttonText.text = "Restart";

                break;

            // ...if the state is on Restart...
            case GameState.Restart:
                
                // ...then Change the state to Start...
                _currentGameState = GameState.Start;

                // ...then Disable all Avatars...
                DisableAllAvatars();

                // ...and Change the Banner and Button Text.
                Banner.text = "Click Start To Begin";
                _buttonText.text = "Start";

                break;

            // ...if the state is on Countdown...
            case GameState.Countdown:
                // ...then do nothing.
                break;

            // ...if the state is on Choose...
            case GameState.Choose:
                // ...then do nothing.
                break;

            // ...if the state is on Result...
            case GameState.Result:
                // ...then do nothing.
                break;

            // ...if the game state is non of the enums...
            default:
                // ...then Throw an Argument out of range exception.
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

        //Get random int arrays for both sprite and name sequences
        var randomSpriteSequence = GetRandomIntArray(Avatars.Count, Headshots.Count);
        var randomNameSequence = GetRandomIntArray(Avatars.Count, Names.Count);

        // For each Avatar Actor Component in the List...
        for (var i = 0; i < Avatars.Count; i++) {
            
            // ... Get a new sprtie and name...
            var sprite = Headshots[randomSpriteSequence[i]];
            var avatarName = Names[randomNameSequence[i]];

            // ... and store the two random variables in a new AvatarPair...
            _storedAvatarPairs.Add(new AvatarPair(sprite, avatarName));

            // ...and set the avatar on screen to that respective pair.
            Avatars[i].SetAvatar(sprite, avatarName);
        }
    }

    #endregion
}
