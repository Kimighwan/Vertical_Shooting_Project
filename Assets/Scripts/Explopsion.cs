 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explopsion : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Invoke("Disable", 2f);
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }
    public void StartExplosion(string target)
    {
        animator.SetTrigger("OnExplosion");

        switch(target)
        {
            case "A":
                transform.localScale = Vector3.one * 0.7f;
                break;
            case "P":
            case "B":
                transform.localScale = Vector3.one * 1f;
                break;
            case "C":
                transform.localScale = Vector3.one * 2f;
                break;
            case "Boss":
                transform.localScale = Vector3.one * 3f;
                break;
        }
    }
}
