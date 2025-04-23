using Com.IsartDigital.ProjectName;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Extension
{
    #region AnimatedSprite2D
    /// <returns> return returns a list containing all animations containing specific names</returns>
    public static List<string> ListAnimationWithName(this AnimatedSprite2D pAnimatedSprite, params string[] pName)
    {
        if (pAnimatedSprite == null) return null;
        List<string> lResult = new List<string>();
        List<string> lAnimations = new List<string>(pAnimatedSprite.SpriteFrames.GetAnimationNames());
        foreach (string lAnimationName in lAnimations)
        {
            if (pName.All(name => lAnimationName.Contains(name)))
            {
                lResult.Add(lAnimationName);
            }
        }
        return (lResult.Count == Prop.ZERO) ? null : lResult;
    }

    /// <returns> returns a list containing all animations containing a specific name</returns>
    public static List<string> ListAnimationWithName(this AnimatedSprite2D pAnimatedSprite, string pName)
    {
        if (pAnimatedSprite == null) return null;
        List<string> lResult = new List<string>();
        List<string> lAnimations = new List<string>(pAnimatedSprite.SpriteFrames.GetAnimationNames());
        foreach (string lAnimationName in lAnimations)
        {
            if (lAnimationName.Contains(pName))
            {
                lResult.Add(lAnimationName);
            }
        }
        return (lResult.Count == 0) ? null : lResult;
    }
    #endregion

    #region Node2D
    public static bool ExitScreen(this Node2D pNode, Vector2 pScreenSize, float pMargin = default)
    {
        return pNode.Position.X < Prop.ZERO - pMargin || pNode.Position.Y < Prop.ZERO - pMargin || pNode.Position.X > pScreenSize.X + pMargin || pNode.Position.Y > pScreenSize.Y + pMargin;
    }

    /// <returns> returns the most distant parent sharing the same class </returns>
    public static T GetFirstParentWithClasse<T>(this T pChild) where T : Node
    {
        T lPapa = pChild;
        while (true)
        {
            if (lPapa.GetParent() is T lDac) lPapa = lDac;
            else break;
        }
        return lPapa;
    }
    #endregion

    #region Node
    /// <summary> out is the number of intermediate parents </summary>
    /// <returns> returns the most distant parent sharing the same class </returns>
    public static T GetFirstParentWithClasse<T>(this T pChild, out int pNParent) where T : Node
    {
        T lFirstParent = pChild;
        int PNDepth = Prop.ZERO;
        while (true)
        {
            if (lFirstParent.GetParent() is T lResult)
            {
                lFirstParent = lResult;
                PNDepth++;
            }
            else break;
        }
        pNParent = PNDepth;
        return lFirstParent;
    }
    #endregion

    #region IEnumerable
    /// <summary> returns an object from the random list </summary>
    public static T RandObj<T>(this IEnumerable<T> pListe, RandomNumberGenerator pRand)
    {
        return pListe.ElementAt(pRand.RandiRange(Prop.ZERO, pListe.Count() - Prop.ONE));
    }
    #endregion

    #region Action
    public static bool Contain<T>(this Action<T> pAction, string pMethodName, out Delegate pDelegate)
    {
        foreach(Delegate lMethod in pAction?.GetInvocationList())
        {
            if(lMethod.Method.Name == pMethodName)
            {
                pDelegate = lMethod;
                return true;
            }
        }
        pDelegate = null;
        return false;
    }

    public static bool Contain(this Action pAction, string pMethodName, out Delegate pDelegate)
    {
        foreach (Delegate lMethod in pAction?.GetInvocationList())
        {
            if (lMethod.Method.Name == pMethodName)
            {
                pDelegate = lMethod;
                return true;
            }
        }
        pDelegate = null;
        return false;
    }
    #endregion
}
