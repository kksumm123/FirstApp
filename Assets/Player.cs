using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator animator;
    int point;
    Text pointText;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pointText = GameObject.Find("Canvas").transform.Find("PointText").GetComponent<Text>();
        AddPoint(0);

        animator.Play("Idle");
        point = PlayerPrefs.GetInt("point", 0);
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            animator.Play("Attack");
            AddPoint(10);
        }
    }

    private void AddPoint(int addValue)
    {
        point += addValue;
        pointText.text = point.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("point", point);
        PlayerPrefs.Save();
    }
}
