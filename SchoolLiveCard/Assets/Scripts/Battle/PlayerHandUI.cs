using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandUI : MonoBehaviour
{
    private List<GameObject> cards;
    public Transform leftPoint;
    public Transform rightPoint;

    
    public void AddCard(GameObject card)
    {
        card.transform.SetParent(this.transform);
        if (cards == null)
        {
            cards = new List<GameObject>();
        }
        cards.Add(card);
        UpdateHandUI();
    }

    public void RemoveCard(GameObject card)
    {
        cards.Remove(card);
        UpdateHandUI();
    }

    private void UpdateHandUI()
    {
        int count = cards.Count;

        if (count == 1)
        {
            Vector3 distance = rightPoint.position - leftPoint.position;
            cards[0].transform.position = leftPoint.position + distance / 2f;
            cards[0].transform.localScale = Vector3.one;
        }
        if (count > 1)
        {
            Vector3 distance = rightPoint.position - leftPoint.position;
            Vector3 intervalDistance = distance / (count - 1);
            for (int i = 0; i < count; i++)
            {
                cards[i].transform.position = leftPoint.position + i * intervalDistance;
                cards[i].transform.localScale = Vector3.one;
            }
        }

    }
}
