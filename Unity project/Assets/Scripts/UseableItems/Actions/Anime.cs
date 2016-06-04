using UnityEngine;
using UnityEngine.UI;


public class Anime : Useable, Cuttable
{
    public delegate void ChainUse();
    public static event ChainUse ChainUsed;

    public GameObject chain;
    private Animator animator;

    override protected void doUse()
    {
        animator = (Animator)chain.GetComponent<Animator>();

        animator.SetTrigger("OTWORZ");
        if (ChainUsed != null)
            ChainUsed();
    }
}