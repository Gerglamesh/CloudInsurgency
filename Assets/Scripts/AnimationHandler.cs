using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {

    private Animator animator;
    private bool IsDamaged;
    private bool IsDead;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("DestroyAnimation"))
        {
            AutoDestroy();
        }
	}

    void AutoDestroy()
    {
        Destroy(this.gameObject);
    }
}
