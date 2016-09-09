using UnityEngine;
using mono = UnityEngine.MonoBehaviour;
using System.Collections;

abstract public class Card	{
	
	#region CARD:FIELDS
	private static int nextIDF = 100; //100 is meant to make it start at three digits
	#endregion

	#region CARD:PROPERTIES
	public static int nextID {
		get { return nextIDF; }
		set { nextIDF = value; }
	}
	#endregion

	#region CARD:ABSTRACT-MEMBERS
	abstract public int 	id { get; }
	abstract public string 	imageName { get; }
	abstract public int		power { get; }
	#endregion

	#region CARD:METHODS
	protected static int CreateCardID(){ nextID++; return nextID; }


	public static void Revert(){
		//probably check the type (i.e. ElementCard, etc.) then look up vanilla card
		//Update: I'm thinking that if we have cards transform, then we just have a linked list
		//of cards for that card.
	}

	public override string ToString(){
		return imageName + ", id: " + id.ToString();
	}

	public static Card operator+(Card former, Card latter){
		mono.print ("0");
		if (former is ElementCard && latter is ElementCard){
			ElementCard f = (ElementCard)former;
			ElementCard l = (ElementCard)latter;
			mono.print (f.ToString());
			ElementCard result = new ElementCard(f.element + l.element);
			mono.print ("1");
			return (Card)result;
		}
		//Other possibilities for card combinations yet to be implemented
		return null;
	}
	#endregion

}
