using UnityEngine;
using System;
using System.Collections;

public class GameCard : MonoBehaviour {

	public GameObject prefab;

	private static ElementGallery elementGallery;
	private static GameManager gameManager;
	private static Transmutor transmutor;

	public bool enteredTransmutor = false;

	private Card cardf;
	private SpriteRenderer rend;
	private Vector3 mousePos;		//the position of the mouse

	public Card card{
		get { return cardf; }
		set {
			cardf = value;
			UpdateCard ();
		}
	}

	void Awake(){
		gameManager		= GameObject.FindObjectOfType<GameManager>();
		elementGallery	= GameObject.FindObjectOfType<ElementGallery>();
		rend			= gameObject.GetComponent<SpriteRenderer> ();
		transmutor		= GameObject.FindObjectOfType<Transmutor> ();
	}

	private void UpdateCard(){
		rend.sprite = elementGallery.GetSprite (card.imageName);
	}

	void OnMouseDrag(){

		//Update the position of the card
		transform.position = new Vector2 (gameManager.mousePos.x, gameManager.mousePos.y); //card jumps once clicked currently, not desired, revisit
	}

	void OnMouseUp(){

		if (enteredTransmutor) {
			transmutor.Transmutation += OnTransmutation;
			transmutor.Add (card);
			transform.parent = transmutor.transform;
			rend.enabled = false;
		} else {
			//Return the card to resting position, smoothly
			StartCoroutine (smoothToRest());
		}
	}

	//Causes the card to smoothly travel back to resting position
	IEnumerator smoothToRest(){  //don't really feel like I fully understand how yield works and how it ties into coroutines, need to come back to it
		while (transform.position != transform.parent.position) {
			transform.position = Vector2.Lerp (transform.position, transform.parent.position, 0.05f);

			yield return null;
		}
	}

	public void OnTransmutation(object source, EventArgs args){
		if (transform.parent = transmutor.transform) { //just in case, might not be needed later
			transmutor.Transmutation -= this.OnTransmutation;
			Destroy(gameObject);
		}
	}
		
}
