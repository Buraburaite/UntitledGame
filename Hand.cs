using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour{

	#region Hand:FIELDS
	public GameObject cardStackPrefab;
	public GameObject gameCardPrefab;
	private Deck deckf;

	private int				maxSizeF;
	private CardStack 		pivotStackF;
	private List<CardStack> stacksF;
	#endregion

	#region Hand:PROPERTIES
	public Deck deck {
		get { return deckf; }
		set { deckf = value; }
	}
	private CardStack pivotStack {
		get { return pivotStackF; }
		set { pivotStackF = value; }
	}
	private List<CardStack> stacks {
		get { return stacksF; }
		set { stacksF = value; }
	}

	public int size	{
		get { return stacks.Count; }
	}
	public int maxSize {
		get { return maxSizeF; }
		set { maxSizeF = value; }
	}
	#endregion

	#region Hand:CONSTRUCTORS
	void Awake() {
		deck = GameObject.FindObjectOfType<Deck> (); //TODO: replace with persistent data
		//Unity tutorials: Persistence - Saving and Loading Data

		maxSize = 5;
		//pivotStack = new CardStack(0f); //TODO: update this number eventually
		stacks = new List<CardStack>();

		float[] posArray = CalcStackPositions (maxSize);

		for (int i = 0; i < maxSize; i++) {
			AddStack (posArray[i]);
		}
	}
	#endregion

	#region Hand:UPDATE
	void Update() {
	}
	#endregion

	#region Hand:METHODS
	//TODO: make this actually calculate
	private void AddStack(float xPos){ //TODO: add Card card param once I have those
		GameObject s = Instantiate (cardStackPrefab, transform.position, Quaternion.identity) as GameObject;
		s.transform.parent = transform;
		s.transform.position = new Vector3 (transform.position.x - 9 + xPos, transform.position.y, transform.position.z);
		stacks.Add (s.GetComponent<CardStack> ()); //9 is right, but really should attach a sprite to get the size so it'll scale
	}
	private static float[] CalcStackPositions(int count = 1){
		float cardSize = 3f; //if cards resized later, even programmatically, this needs to be updated to reflect that
		float buffer = (18 - (cardSize * count)) / (count + 1);

		float[] posArray = new float[count];
		for (int i = 0; i < count; i++) {
			posArray[i] = buffer + ((cardSize + buffer) * i) + (cardSize / 2);
		}

		return posArray;
	}
	private void RemoveStack(Card card){
	}
	private void SwitchStacks(CardStack a, CardStack b){

	}

	private int DrawDecision(Card card){
		int verdict = 100; //100 means "do not draw"
		for (int i = 0; i < size; i++) {
			if (stacks [i].count > 0) {
				if (stacks [i].Peek ().card.imageName == card.imageName) {
					return i; //a positive number other than 100 means "draw, place in stacks[i]"
				}
			} else {
				verdict -= 500;//a negative number means "draw, place in first empty stack"
			}
		}
		return verdict;
	}

	public void Draw(Card card, int verdict){
		if (verdict != 100) {
			if (verdict < 0) {
				foreach (CardStack s in stacks) {
					if (s.count == 0) {
						s.Push (card);
						break;
					}
				}
			} else {
				stacks [verdict].Push (card);
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(18f, 6f, 0f));
	}

	public void Refill(){
		int verdict = DrawDecision (deck.Peek ());
		while (verdict != 100) {
			Draw (deck.Pop(), verdict);
			verdict = DrawDecision (deck.Peek ());
		}
	}
	#endregion


}
