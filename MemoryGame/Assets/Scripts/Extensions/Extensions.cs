using System;
using UnityEngine;

public static class Extensions {
    
    /// <summary>
    /// Sets the x position of the transform to the float passed
    /// </summary>
    /// <param name="t">The transform to set</param>
    /// <param name="x">The new x value</param>
    public static void SetPositionX(this Transform t, float x) {
        t.position = new Vector3(x, t.position.y, t.position.z);
    }

    /// <summary>
    /// Returns an array of all Transforms that are children of this transform
    /// </summary>
    /// <param name="t">The transform to search</param>
    /// <returns>A list of all transforms that are children of this transform</returns>
    public static Transform[] GetChildren(this Transform t) {
        return t.GetComponentsInChildren<Transform>();
    }

    /// <summary>
    /// Returns an array of all GameObjects that are children of this transform
    /// </summary>
    /// <param name="t">The transform to search</param>
    /// <returns>An array of all gameobjects that are children of this transform</returns>
    public static GameObject[] GetChildrenAsGameObjects(this Transform t) {
        
        // Gets all children of this transform
        var transformChildren = t.GetChildren();

        // Make a GameObject array with the length of the amount of children
        var returnGameObjects = new GameObject[transformChildren.Length];

        //For each child transform...
        for (var i = 0; i < transformChildren.Length; i++) {
            
            // ...then get that transforms gameObject and place it into the GameObject array
            returnGameObjects[i] = t.GetChild(i).gameObject;
        }

        //Return the array of GameObjects
        return returnGameObjects;
    }

    /// <summary>
    /// Returns an array of all Transforms that are children of this transform
    /// </summary>
    /// <param name="t">The transform to search</param>
    /// <returns>A list of all transforms that are children of this transform</returns>
    public static Transform[] GetImmediateChildren(this Transform t) {

        //Creates a Transform Array with a length of this transforms immediate children length
        var transforms = new Transform[t.childCount];

        // For each immediate child of this transform...
        for (var i = 0; i < transforms.Length; i++) {
            
            // ...Add that immediate child to the Transform array created
            transforms[i] = t.GetChild(i);
        }
        
        //Return the Transform Array
        return transforms;
    }

    /// <summary>
    /// Returns an array of all GameObjects that are immediate children of this transform
    /// </summary>
    /// <param name="t">The transform to search</param>
    /// <returns>An array of all gameobjects that are children of this transform</returns>
    public static GameObject[] GetImmediateChildrenAsGameObjects(this Transform t) {

        // Get an array of all immediate children of this transform
        var transformChildren = t.GetImmediateChildren();

        // Create a GameObject Array with the length of the Transform Array
        var returnGameObjects = new GameObject[transformChildren.Length];

        // For each element in the Transform Array...
        for (var i = 0; i < transformChildren.Length; i++) {
            
            // ...Get the GameObject attached to the transform and store it in the GameObject array
            returnGameObjects[i] = t.GetChild(i).gameObject;
        }

        //Return the Array of GameObjects
        return returnGameObjects;
    }

    /// <summary>
    /// Finds an immediate child of this transform by a given tag
    /// </summary>
    /// <param name="t">The transform to extend</param>
    /// <param name="tag">The tag to search by</param>
    /// <returns>The found gameobject</returns>
    public static Transform FindChildByTag(this Transform t, string tag) {

        // Get all Transform Children of this transform
        var children = t.GetChildren();

        // For each Transform in the Transform Array...
        for (var i = 0; i < children.Length; i++) {

            // ...Store the Transform...
            var child = children[i];

            // ...And check if the Transforms tag matches the passed tag...
            if (child.CompareTag(tag)) {
                
                // ...Then return that Transform.
                return child;
            }
        }

        // If no child is found, return null
        return null;
    }

    /// <summary>
    /// Sets the parent of the given gameobject as the other gameobject passed
    /// </summary>
    /// <param name="g">The gameobject to set the parent of</param>
    /// <param name="parent">The parent gameobject</param>
    public static void SetParent(this GameObject g, GameObject parent) {
        g.transform.parent = parent.transform;
    }

    /// <summary>
    /// Destroys the passed gameobject as long as it exists
    /// </summary>
    /// <param name="g">The gameObject to be destroyed</param>
    public static void SafeDestroy(this GameObject g) {
        
        // If the gameObject this is method is called from is not null...
        if (g != null) {
            
            // ...Then Destroy the GameObject.
            GameObject.Destroy(g);

        }
    }

    /// <summary>
    /// Calls the delegate if it is not null
    /// </summary>
    /// <param name="action">The action to be called</param>
    public static void Run(this Action action) {
        
        // If the Action that called this Method is not null...
        if (action != null) {
            
            // ...Then call any attached Functions.
            action();

        }
    }

    /// <summary>
    /// Calls the delegate if it is not null
    /// </summary>
    /// <param name="action">The action to be called</param>
    /// <param name="parameter">The parameter to be passed to the Action of T type</param>
    public static void Run<T>(this Action<T> action, T parameter) {

        // If the Action that called this Method is not null... 
        if (action != null) {
            
            // ...Then call any attached Function and pass the given parameter.
            action(parameter);

        }
    }

    /// <summary>
    /// Calls the delegate if it is not null and returns true if the func is null or if all invocations return true
    /// </summary>
    /// <param name="func">The func to be called</param>
    public static bool RunOrIsNull(this Func<bool> func) {
        
        // If the Func that called this Method is null...
        if (func == null) {

            // ...Then return true, as no functions where attached, which maintains decoupling and functionality. 
            return true;

        }

        // Store all attached Functions to this Func in an Array
        var functions = func.GetInvocationList() as Func<bool>[];

        // If the functions array is null...
        if (functions == null) {
            
            // ...Then return true, as no functions where attached, which maintains decoupling and functionality. 
            return true;

        }

        // For each Func in the Func Array...
        for (var i = 0; i < functions.Length; i++) {
            
            // ...If the function attached returns false...
            if (functions[i]() == false) {

                // ...Then return false, as no single function must false for this function to return true.
                return false;
            }
        }

        // Return true, meaning all attached functions to this Func returned true.
        return true;
    }

    /// <summary>
    /// Calls the delegate if it is not null and returns true if the func is null or if all invocations return true
    /// </summary>
    /// <param name="func">The func to be called</param>
    /// <param name="passedParameter">The passed parameter of the Func</param>
    public static bool RunOrIsNull<T>(this Func<T, bool> func, T passedParameter) {
        
        // If the Func that called this Method is null...
        if (func == null) {

            // ...Then return true, as no functions where attached, which maintains decoupling and functionality. 
            return true;

        }

        // Store all attached Functions to this Func in an Array
        var functions = func.GetInvocationList() as Func<T, bool>[];

        // If the functions array is null...
        if (functions == null) {

            // ...Then return true, as no functions where attached, which maintains decoupling and functionality. 
            return true;

        }

        // For each Func in the Func Array...
        for (var i = 0; i < functions.Length; i++) {

            // ...If the function attached returns false...
            if (functions[i](passedParameter) == false) {

                // ...Then return false, as no single function must false for this function to return true.
                return false;
            }
        }

        // Return true, meaning all attached functions to this Func returned true.
        return true;

    }
}