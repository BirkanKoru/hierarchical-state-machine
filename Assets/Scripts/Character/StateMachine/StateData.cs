using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateData
{
    public Character character { get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody rigidbody { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }

    public StateData(Character character = null, Animator animator = null, Rigidbody rigidbody = null, CapsuleCollider capsuleCollider = null)
    {
        this.character = character;
        this.animator = animator;
        this.rigidbody = rigidbody;
        this.capsuleCollider = capsuleCollider;
    }
}
