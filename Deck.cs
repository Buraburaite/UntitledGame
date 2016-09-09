using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

	private List<Card> cardsf;

	public List<Card> cards{
		get { return cardsf; }
		private set{
			cardsf = value;
		}
	}

	void Awake(){
		cards = new List<Card> ();
	}

	public void AddCard(Card card, int pos = int.MaxValue){
		if (pos == int.MaxValue) {
			cards.Add (card);
		} else {
			cards.Insert (pos, card);
		}
	}

	public Card Peek(){
		return cards [0];
	}
		
	public Card Pop(){
		Card card = cards[0];
		cards.RemoveAt(0);
		return card;
	}

	public override string ToString ()
	{
		string str = "[";
		foreach (Card card in cards) {
			str += card.imageName + ", ";
		}
		str.Remove (str.Length - 2);
		str += "]";
		return str;
			
	}
}
