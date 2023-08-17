using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchInputManager : MonoBehaviour
{
    public static TouchInputManager instance;

    private void Awake()
    {
        instance = this;
    }

    public TMP_Text onScreenText, levelText;
    public GameObject bookObject;
    public float dragDistance = 20f;
    public bool canOpen = true;
    
    [HideInInspector] 
    public int levelNo = 1; 

    Vector2 firstPoint;
    Vector2 lastPoint;

    private void Start()
    {
        canOpen = true;
        onScreenText.text = "Touch to OPEN";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if(canOpen)
                {
                    openBook();
                }
                firstPoint = touch.position;
                lastPoint = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastPoint = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastPoint = touch.position;

                if (Mathf.Abs(lastPoint.x - firstPoint.x) > Mathf.Abs(lastPoint.y - firstPoint.y)) // Horizontal Swipe
                {
                    if (Mathf.Abs(lastPoint.x - firstPoint.x) > dragDistance)
                    {
                        if (lastPoint.x - firstPoint.x > 0)
                        {
                            if(levelNo > 1 && levelNo <= 5 && !bookObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Book_PreviousPage") && !bookObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Book_NextPage"))
                                leftSwipe();
                        }
                        else
                        {
                            if (levelNo < 5 && levelNo >= 1 && !bookObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Book_PreviousPage") && !bookObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Book_NextPage"))
                                rightSwipe();
                        }
                    }
                }

            }
        }
    }

    void openBook()
    {
        bookObject.GetComponent<Animator>().SetTrigger("OpenBook");
        onScreenText.text = "Swipe to turn the page";
        levelText.text = "Level " + levelNo.ToString();
        canOpen = false;
    }

    void leftSwipe()
    {
        levelNo--;
        bookObject.GetComponent<Animator>().SetTrigger("PreviousPage");
    }

    void rightSwipe()
    {
        levelNo++;
        bookObject.GetComponent<Animator>().SetTrigger("NextPage");
    }

}
