using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Transmutor : MonoBehaviour {

	public event EventHandler Transmutation; //not sure what I'm going to use this for...

	public TextAsset eRecipesCSV; //needed for Element.CreateERecipes
	public GameObject gameCardPrefab;

	private List<Card> componentsf;

	public List<Card> components{
		get { return componentsf; }
		set { componentsf = value; }
	}

	public void Add(Card card){
		components.Add (card);
//		if (components.Count == 2){
//			GameObject gob = Instantiate (gameCardPrefab, transform.position, Quaternion.identity) as GameObject;
//			GameCard gCard = gob.GetComponent<GameCard> ();
//			ElementCard result = (components [0] + components [1]) as ElementCard;
//		}
//		OnTransmutation();
	}

	void Awake(){
		components = new List<Card> ();

	}

	protected virtual void OnTransmutation(){
		if (Transmutation != null) {
			Transmutation (this, EventArgs.Empty);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag("GameCard")){
			col.GetComponent<GameCard> ().enteredTransmutor = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag("GameCard")){
			col.GetComponent<GameCard> ().enteredTransmutor = false;
		}
	}
}
