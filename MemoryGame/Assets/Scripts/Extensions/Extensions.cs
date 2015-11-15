using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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
        var transformChildren = t.GetChildren();
        var returnGameObjects = new GameObject[transformChildren.Length];
        for (var i = 0; i < transformChildren.Length; i++) {
            returnGameObjects[i] = t.GetChild(i).gameObject;
        }
        return returnGameObjects;
    }

    /// <summary>
    /// Returns an array of all Transforms that are children of this transform
    /// </summary>
    /// <param name="t">The transform to search</param>
    /// <returns>A list of all transforms that are children of this transform</returns>
    public static Transform[] GetImmediateChildren(this Transform t) {
        var transforms = new Transform[t.childCount];
        for (var i = 0; i < transforms.Length; i++) {
            transforms[i] = t.GetChild(i);
        }
        return transforms;
    }

    /// <summary>
    /// Returns an array of all GameObjects that are immediate children of this transform
    /// </summary>
    /// <param name="t">The transform to search</param>
    /// <returns>An array of all gameobjects that are children of this transform</returns>
    public static GameObject[] GetImmediateChildrenAsGameObjects(this Transform t) {
        var transformChildren = t.GetImmediateChildren();
        var returnGameObjects = new GameObject[transformChildren.Length];
        for (var i = 0; i < transformChildren.Length; i++) {
            returnGameObjects[i] = t.GetChild(i).gameObject;
        }
        return returnGameObjects;
    }

    /// <summary>
    /// Finds an immediate child of this transform by a given tag
    /// </summary>
    /// <param name="t">The transform to extend</param>
    /// <param name="tag">The tag to search by</param>
    /// <returns>The found gameobject</returns>
    public static Transform FindChildByTag(this Transform t, string tag) {
        var children = t.GetChildren();
        for (var i = 0; i < children.Length; i++) {
            var child = children[i];
            if (child.CompareTag(tag)) {
                return child;
            }
        }
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
        if (g != null) {
            Object.Destroy(g);
        }
    }

    /// <summary>
    /// Calls the delegate if it is not null
    /// </summary>
    /// <param name="action">The action to be called</param>
    public static void Run(this Action action) {
        if (action != null) {
            action();
        }
    }

    /// <summary>
    /// Calls the delegate if it is not null
    /// </summary>
    /// <param name="action">The action to be called</param>
    /// <param name="parameter">The parameter to be passed to the Action of T type</param>
    public static void Run<T>(this Action<T> action, T parameter) {
        if (action != null) {
            action(parameter);
        }
    }

    /// <summary>
    /// Calls the delegate if it is not null and returns true if the func is null or if all invocations return true
    /// </summary>
    /// <param name="func">The func to be called</param>
    public static bool RunOrIsNull(this Func<bool> func) {
        if (func == null) {
            return true;
        }

        var functions = func.GetInvocationList();

        for (var i = 0; i < functions.Length; i++) {
            if (((Func<bool>) functions[i])() == false) {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Calls the delegate if it is not null and returns true if the func is null or if all invocations return true
    /// </summary>
    /// <param name="func">The func to be called</param>
    /// <param name="passedParameter"></param>
    public static bool RunOrIsNull<T>(this Func<T, bool> func, T passedParameter) {
        if (func == null) {
            return true;
        }

        var functions = func.GetInvocationList() as Func<T, bool>[];

        if (functions == null) {
            return true;
        }

        for (var i = 0; i < functions.Length; i++) {
            if (functions[i](passedParameter) == false) {
                return false;
            }
        }
        return true;
    }
}