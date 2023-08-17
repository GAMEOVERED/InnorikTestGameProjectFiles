using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    public Sprite PageImage;
    public List<Sprite> Numbers;
    public int PageNumber;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPoistion;

    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;


    

    // Update is called once per frame
    private void Update()
    {

        /*  if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
          {
              startTouchPosition = Input.GetTouch(0).position;
          }
          if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
          {
              endTouchPoistion = Input.GetTouch(0).position;
              if(endTouchPoistion.x < startTouchPosition.x)
              {
                  NextPage();
              }
              if (endTouchPoistion.x > startTouchPosition.x)
              {
                  PreviousPage();
              }
          }*/


        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for(int i = 0; i<pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for(int i = 0; i < pos.Length; i++)
            {
                if(scroll_pos < pos[i] + (distance/2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

    }

    private void PreviousPage()
    {
        PageNumber--;
       // PageImage.sprite = Numbers[PageNumber];
    }

    private void NextPage()
    {
        PageNumber++;
        //PageImage.sprite = Numbers[PageNumber];
    }
}
