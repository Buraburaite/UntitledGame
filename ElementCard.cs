using System;

public class ElementCard : Card {

	#region ELEMENTCARD:FIELDS
	//Note: All fields have properties, and vice-versa. Field names end with an F.
	private int idf;
	private int powerf;
	private Element elementf;
	#endregion

	#region ELEMENTCARD:PROPERTIES
	public Element element {
		get { return elementf; }
	}
	#endregion

	#region ELEMENTCARD:OVERRIDDEN-MEMBERS
	public override int 	id { //in constructor change varf directly, abstract restrictions
		get { return idf; }
	}
	public override string 	imageName {
	get { return element.name;	}
	}
	public override int		power {
	get { return 0;	}
	}
	#endregion

	#region ELEMENTCARD:CONSTRUCTORS
	public ElementCard(Element e){
		elementf = e;
		idf = Card.CreateCardID();
	}
	public ElementCard(string e){
		elementf = (Element)e; //add straightforward "Sound" -> Element.sound to recipeDict
		idf = Card.CreateCardID();
	}
	#endregion

	#region ELEMENTCARD:METHODS


	#endregion
}
