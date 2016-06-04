using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Cuttable { }

public class CutChain : Pickable
{
    private List<Useable> readyToCut = new List<Useable>();
    override protected void doUse()
    {
        foreach (var u in readyToCut)
        {
            u.Use();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Useable u = other.gameObject.GetComponent<Useable>();
        if (u is Cuttable)
        {
            u.CanBeUsed = true;
            u.HighlightItem();
            readyToCut.Add(u);
        }
    }
    void OnTriggerExit(Collider other)
    {
        Useable u = other.gameObject.GetComponent<Useable>();
        if (u is Cuttable)
        {
            u.CanBeUsed = false;
            u.UnHighlightItem();
            readyToCut.Remove(u);
        }
    }
}

