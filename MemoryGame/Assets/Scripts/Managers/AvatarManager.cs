using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AvatarManager : Manager<AvatarManager> {

    #region Public Variables
    [Tooltip("Sprites that can be used as headshots for different people")]
    public List<Sprite> Headshots;

    [Tooltip("Strings that are used as names for the different people")]
    public List<string> Names;

    [Tooltip("The Avatars that will nead headshots and names")]
    public List<ActorAvatar> Avatars;

    [Tooltip("The Banner to display directions for the user")]
    public Text Banner;
    
    [Tooltip("The Button to push to move states forward")]
    public Button ActionButton;

    private Text _buttonText;   //The Text within the ActionButton

    private List<AvatarPair> _storedAvatarPairs = new List<AvatarPair>(); //Collection of AvatarPair objects that will be within Avatars
    #endregion

    void Start() {
        _buttonText = ActionButton.GetComponentInChildren<Text>();
    }

    #region Public Methods
   
    /// <summary>
    /// Randomizes the headshot and names for all ActorAvatars in the Avatars array.
    /// </summary>
    void RandomizeAllAvatars() {
        var randomizedNameOrder = ArrayHelpers.GetRandomIntArray(_storedAvatarPairs.Count);
        var randomizedSpirteOrder = ArrayHelpers.GetRandomIntArray(_storedAvatarPairs.Count);

        // For each ActorAvatar in the Avatars array...
        for (var i = 0; i < Avatars.Count; i++) {
            
            // ...Randomize the headshot and name.
            Avatars[i].SetAvatar(_storedAvatarPairs[randomizedNameOrder[i]].HeadShot, _storedAvatarPairs[randomizedSpirteOrder[i]].Name);
        }
    }

    /// <summary>
    /// Enables all Avatars on Screen
    /// </summary>
    private void EnableAllAvatars() {
        SetAvatarsEnabled(true);
    }

    /// <summary>
    /// Disables All Avatars on Screen
    /// </summary>
    private void DisableAllAvatars() {
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
    
    public void ActionClick() {
        GameStateManager.Instance.CurrentProgressState.ActionTrigger();
    }

    /// <summary>
    /// Sets the list avatars to each a new random AvatarPair
    /// </summary>
    private void SetNewAvatars() {
        
        // Clear out other stored Avatar Pairs
        _storedAvatarPairs.Clear();

        //Get random int arrays for both sprite and name sequences
        var randomSpriteSequence = ArrayHelpers.GetRandomIntArray(Avatars.Count, Headshots.Count);
        var randomNameSequence = ArrayHelpers.GetRandomIntArray(Avatars.Count, Names.Count);

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
    
    void OnEnable() {
        GameStateManager.Instance.CurrentProgressState.OnStart += StartState;
        GameStateManager.Instance.CurrentProgressState.OnMemorize += MemorizeState;
        GameStateManager.Instance.CurrentProgressState.OnRestart += RestartState;
    }
    
    void OnDisable() {
        GameStateManager.Instance.CurrentProgressState.OnStart -= StartState;
        GameStateManager.Instance.CurrentProgressState.OnMemorize -= MemorizeState;
        GameStateManager.Instance.CurrentProgressState.OnRestart -= RestartState;
    }
    
    void MemorizeState() {
                
                // Enable all disabled Avatars
                EnableAllAvatars();

                // Randomize and Save the new Avatar headshots and names
                SetNewAvatars();

                // Update the text in the Button and Banner.
                Banner.text = "Memorize!";
                _buttonText.text = "Go!";
    }
    
    void RestartState() {
                // Randomize all avatars (This will need to be updated)...
                RandomizeAllAvatars();

                //  Update the banner and button Text.
                Banner.text = "Choose the Correct Pairs!";
                _buttonText.text = "Restart";
    }
    
    void StartState() {
                
                // Disable all Avatars
                DisableAllAvatars();

                // Change the Banner and Button Text.
                Banner.text = "Click Start To Begin";
                _buttonText.text = "Start";
    }
}
