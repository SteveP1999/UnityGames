using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region Variables
    private Vector3 _dragOffset;
    private Camera _cam;
    [SerializeField] private int borderNumber = 0;
    private Vector3 startingPos;
    [SerializeField] private bool canDrag = false;
    [SerializeField] private float _speed = 100;
    #endregion

    void Awake()
    {
        _cam = Camera.main;
    }

    public void setDrag(bool val)
    {
        canDrag = val;
    }

    Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            startingPos = transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
            _dragOffset = transform.position - GetMousePos();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
        }
    }

    private Border findNewPlace()
    {
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
        for (int i = 0; i < borders.Length; i++)
        {
            if (!borders[i].GetComponent<Border>().getOccupied() && borders[i].GetComponent<Border>().getIsStarter())
            {
                return borders[i].GetComponent<Border>();
            }
        }
        return null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Border newBorder = findNewPlace();

        if (canDrag)
        {
            GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Parent");

            if (calculateDistance() < 1.0f)
            {
                Border oldBorder = this.GetComponentInChildren<CardModel>().getBorder();
                Border newestBorder = borders[borderNumber].GetComponent<Border>();

                if (newestBorder.getCard() == null && newestBorder.getIsAvailable())
                {
                    Debug.Log("Case 1");

                    //Old border:
                    oldBorder.setCard(null);
                    oldBorder.setId(-1);
                    oldBorder.setOccupied(false);

                    //Card settings:
                    this.GetComponentInChildren<CardModel>().setBorder(borders[borderNumber].GetComponent<Border>()); 
                    this.GetComponentInParent<ParentScript>().transform.position = borders[borderNumber].GetComponent<Border>().transform.position;

                    //New border settings:
                    newestBorder.setCard(this.GetComponentInChildren<CardModel>());
                    newestBorder.setId(this.GetComponentInChildren<CardModel>().getUniqueCardId());
                    newestBorder.setOccupied(true);
                }
                else
                {
                    if (newBorder == null && newestBorder.getOccupied() != false)
                    {
                        Debug.Log("Case 2");
                        //Old border:
                        oldBorder.setCard(newestBorder.getCard());
                        oldBorder.setId(newestBorder.getCard().GetComponent<CardModel>().getUniqueCardId());

                        //Old card:
                        newestBorder.getCard().setBorder(oldBorder);
                        Vector3 destination = oldBorder.GetComponent<Border>().transform.position;
                        for (int i = 0; i < cards.Length; i++)
                        {
                            if (cards[i].GetComponentInChildren<CardModel>().getUniqueCardId() == borders[borderNumber].GetComponent<Border>().getCard().GetComponent<CardModel>().getUniqueCardId())
                            {
                                GameController.instance.positionForSmoothStep(cards[i], destination.x, destination.y, 0, true, Constants.cardChangeSpeed);
                            }
                        }

                        //New border:
                        newestBorder.setCard(this.GetComponentInChildren<CardModel>());
                        newestBorder.setId(this.GetComponentInChildren<CardModel>().getUniqueCardId());

                        //New card:
                        this.GetComponentInChildren<CardModel>().setBorder(borders[borderNumber].GetComponent<Border>());
                        Vector3 dest = borders[borderNumber].transform.position;
                        for (int i = 0; i < cards.Length; i++)
                        {
                            if (cards[i].GetComponentInChildren<CardModel>().getUniqueCardId() == this.GetComponentInChildren<CardModel>().getUniqueCardId())
                            {
                                GameController.instance.positionForSmoothStep(cards[i], dest.x, dest.y, dest.z, true, Constants.cardChangeSpeed);
                            }
                        }
                    }
                    else
                    {
                        if (newestBorder.getOccupied() == true)
                        {
                            Debug.Log("Case 3");
                            Debug.Log("Bejutottam az old cards settingsbe");

                            //Settings for the border  where the card went:
                            newBorder.GetComponent<Border>().setCard(borders[borderNumber].GetComponent<Border>().getCard().GetComponent<CardModel>());
                            newBorder.GetComponent<Border>().setOccupied(true);
                            newBorder.GetComponent<Border>().setId(borders[borderNumber].GetComponent<Border>().getCard().GetComponent<CardModel>().getUniqueCardId());

                            //Old cards settings:
                            newestBorder.getCard().GetComponent<CardModel>().setBorder(newBorder);
                            Vector3 destination2 = newBorder.transform.position;
                            for (int i = 0; i < cards.Length; i++)
                            {
                                if (cards[i].GetComponentInChildren<CardModel>().getUniqueCardId() == borders[borderNumber].GetComponent<Border>().getCard().GetComponent<CardModel>().getUniqueCardId())
                                {
                                    GameController.instance.positionForSmoothStep(cards[i], destination2.x, destination2.y, destination2.z, true, Constants.cardChangeSpeed);
                                }
                            }
                        }

                        Debug.Log("Case 4");
                        //Old border settings:
                        if (oldBorder != null)
                        {
                            oldBorder.setOccupied(false);
                            oldBorder.setCard(null);
                            oldBorder.setId(-1);
                        }

                        //New card settings
                        Vector3 dest = borders[borderNumber].transform.position;
                        for (int i = 0; i < cards.Length; i++)
                        {
                            if (cards[i].GetComponentInChildren<CardModel>().getUniqueCardId() == this.GetComponentInChildren<CardModel>().getUniqueCardId())
                            {
                                GameController.instance.positionForSmoothStep(cards[i], dest.x, dest.y, dest.z, true, Constants.cardChangeSpeed);
                            }
                        }
                        this.GetComponentInChildren<CardModel>().setBorder(borders[borderNumber].GetComponent<Border>());

                        newestBorder.setOccupied(true);
                        newestBorder.setId(this.GetComponentInChildren<CardModel>().getUniqueCardId());
                        newestBorder.setCard(this.GetComponentInChildren<CardModel>());
                    }
                }
            }
            else
            {
                this.GetComponentInParent<ParentScript>().transform.position = startingPos;
            }
        }
    }

    public double calculateDistance()
    {
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");

        double[] distances = new double[borders.Length];
        for (int i = 0; i < borders.Length; i++)
        {
            float a = (transform.position.x - borders[i].transform.position.x) * (transform.position.x - borders[i].transform.position.x);
            float b = (transform.position.y - borders[i].transform.position.y) * (transform.position.y - borders[i].transform.position.y);
            if (borders[i].GetComponent<Border>().getIsAvailable() == true)
            {
                distances[i] = Math.Abs(Math.Sqrt(a * a + b * b));
            }
            else
            {
                distances[i] = 100000;
            }
        }

        double Min = 100000;
        int k = 0;
        foreach (double number in distances)
        {
            if (number < Min)
            {
                Min = number;
                borderNumber = k;
            }
            k++;
        }
        return Min;
    }
}