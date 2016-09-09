using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardStack : MonoBehaviour{

	#region CardStack:CONSTRUCTORS
	void Awake(){
		cardsf = new Stack<GameCard> ();
	}
	#endregion

	#region CardStack:FIELDS
	//Note: All fields have properties, and vice-versa. Field names end with an F.
	public GameObject prefab;
	private static int		maxSizef = 5;

	private Stack<GameCard> cardsf;
	#endregion

	#region CardStack:PROPERTIES
	private Stack<GameCard> cards 	{
		get { return cardsf; }
		set { cardsf = value; } //Reminder: this is fine since it's private.
	}

	public static int maxSize {
		get { return maxSizef; }
		set { maxSizef = value; }
	}
	public int count {
		get { return cards.Count; }
	}

	public Hand hand {
		get { return transform.parent.GetComponent<Hand> (); }
	}
	#endregion

	#region CardStack:METHODS
	void OnDrawGizmos(){ Gizmos.DrawWireCube (transform.position, new Vector3(3f, 4.4f, 0f)); }

	void OnMouseOver(){
//		print ("hover detected");
	}

	public void Push(Card card){
		GameObject gob = GameObject.Instantiate (hand.gameCardPrefab, transform.position, Quaternion.identity) as GameObject;
		GameCard gCard = gob.GetComponent<GameCard> ();
		gCard.transform.parent = transform;
		gCard.card = card;
		cards.Push (gCard);
	}

	public void Push(GameCard gCard)	{
		if (count < maxSize) {
			gCard.transform.parent = transform;
			cards.Push (gCard);
		} else {
		print("Error: CardStack at position x=" + transform.position.x + " is full.");
		}
	}

	public GameCard Peek() { return cards.Peek(); }
	public void Pop() { cards.Pop (); }
	#endregion
}
