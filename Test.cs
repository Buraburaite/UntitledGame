using UnityEngine;
using System.Collections;
using e = Element;

public class Test : MonoBehaviour {

	public GameObject cardPrefab;

	private Hand hand;
	private Deck deck;

	void Start(){
		GameObject h = GameObject.FindWithTag("Hand");
		hand = h.GetComponent<Hand> ();
		GameObject d = GameObject.FindWithTag ("Deck");
		deck = d.GetComponent<Deck> ();
	}

	public void Do(){

		deck.AddCard (new ElementCard(e.air));
		deck.AddCard (new ElementCard(e.earth));
		deck.AddCard (new ElementCard(e.water));
		deck.AddCard (new ElementCard(e.dark));
		deck.AddCard (new ElementCard(e.megapoda));
		for (int i = 0; i < 40; i++) {
			Element element = e.available [Random.Range (0, e.available.Length)];

			deck.AddCard (new ElementCard(element));
		}

		hand.Refill();
	}
}
