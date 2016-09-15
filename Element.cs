using UnityEngine;
using mono = UnityEngine.MonoBehaviour;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public sealed class Element{

	#region Instances (All)
	public static readonly Element aether 	= new Element("Aether");
	public static readonly Element air 		= new Element("Air");
	public static readonly Element earth	= new Element("Earth");
	public static readonly Element fire 	= new Element("Fire");
	public static readonly Element water	= new Element("Water");

	public static readonly Element dark 	= new Element("Dark");
	public static readonly Element ice		= new Element("Ice");
	public static readonly Element lava 	= new Element("Lava");
	public static readonly Element light 	= new Element("Light");
	public static readonly Element metal	= new Element("Metal");
	public static readonly Element rain		= new Element("Rain");
	public static readonly Element sand 	= new Element("Sand");
	public static readonly Element sound	= new Element("Sound");
	public static readonly Element spark 	= new Element("Spark");
	public static readonly Element steam 	= new Element("Steam");

	public static readonly Element basilisk	= new Element("Basilisk");
	public static readonly Element divine 	= new Element("Divine");
	public static readonly Element dragon 	= new Element("Dragon");
	public static readonly Element effluvian= new Element("Effluvian");
	public static readonly Element fiend	= new Element("Fiend");
	public static readonly Element leviathan= new Element("Leviathan");
	public static readonly Element mech		= new Element("Mech");
	public static readonly Element megapoda	= new Element("Megapoda");
	public static readonly Element spirit 	= new Element("Spirit");
	public static readonly Element stormling= new Element("Stormling");

	public static readonly Element aetherless	= new Element("Aetherless"); //Temporary names
	public static readonly Element airless 		= new Element("Airless");
	public static readonly Element earthless	= new Element("Earthless");
	public static readonly Element fireless 	= new Element("Fireless");
	public static readonly Element waterless	= new Element("Waterless");

	public static readonly Element sidereal = new Element ("Sidereal");

	private static readonly Element init = new Element ("Init"); //calls the Init method
	#endregion

	#region Static Variables
	public static readonly Element[] available = new Element[] {
		aether, air, fire, water,
		dark, ice, light, metal, spark,
		dragon, mech, megapoda, spirit	};
	
	public static readonly List<Element> elements = new List<Element> {
		aether, air, earth, fire, water,
		dark, ice, lava, light, metal, rain, sand, sound, spark, steam,
		basilisk, divine, dragon, effluvian, fiend, leviathan, mech, megapoda, spirit, stormling,
		aetherless, airless, earthless, fireless, waterless,
		sidereal };

	public static readonly List<string> elementStrings = new List<string> {
		"Aether", "Air", "Earth", "Fire", "Water",
		"Dark", "Ice", "Lava", "Light", "Metal", "Rain", "Sand", "Sound", "Spark", "Steam",
		"Basilisk", "Divine", "Dragon", "Effluvian", "Fiend", "Leviathan", "Mech", "Megapoda", "Spirit", "Stormling",
		"Aetherless", "Airless", "Earthless", "Fireless", "Waterless",
		"Sidereal" };

	private static Dictionary<int, Element> recipeDict;
	#endregion

	#region Non-Static Members
	private string namef;
	private Element[] componentsf;
	private int recipe;

	public string name {
		get 		{ return namef; }
		private set { namef = value; }
	}
	public Element[] components{
		get 		{ return componentsf; }
		private set { componentsf = value; }
	}
	#endregion

	#region Methods
	private Element(string str){
		if (str == "Init") {
			TextAsset eRecipesCSV = GameObject.FindObjectOfType<Transmutor> ().eRecipesCSV;
			Init (eRecipesCSV);
		} else {
			name = str;
		}
	}

	public static implicit operator string(Element e){ return e.name; }

	public static explicit operator Element(string str){
		for (int i = 0; i < elementStrings.Count; i++){
			if (string.Equals(str, elementStrings[i], System.StringComparison.OrdinalIgnoreCase)){
				return elements[i];
			}
		}
		UnityEngine.Debug.Log ("Error: \"" + str + "\" is not the name of an element, Origin: Element cast from string");
		return null;
	}

	public static Element operator +(Element former, Element latter)
	{
		return Transmute (new Element[] { former, latter });
	}
		
	private static void Init(TextAsset textAsset){
		
		//Fills out the components member of each element based on eRecipes.csv
		List<string> strList;
		List<string> components;
		string text = textAsset.ToString ();
		string[] array = text.Split('*');
		List<Element> componentsAsElements = new List<Element> ();
		foreach (string str in array) {
			strList = str.Split (',').ToList ();
			components = strList.GetRange (1, strList.Count - 1);
			componentsAsElements.Clear ();
			foreach (string comp in components) {
				componentsAsElements.Add ((Element)comp);
			}
			foreach (Element e in elements) {
				if (e.name.Equals (strList [0])) {
					e.components = componentsAsElements.ToArray ();
				}
			}
		}

		//Using 1 through n powers of n, where n is the max length of any combination of elements
		//(i.e. 5 elements in sidereal = Aether + Air + Earth + Fire + Water),
		//a system of ints where any combination results in a unique sum is achieved.
		//This allows adding elements together by adding the "recipe" values of their components,
		//as implemented in Transmute(Element[] elementArray).
		aether.recipe	= 5;
		air.recipe		= 25;
		earth.recipe	= 125;
		fire.recipe		= 625;
		water.recipe	= 3125;

		recipeDict = new Dictionary<int, Element>();
		int total = 0;
		foreach (Element e in elements) {
			if (e.recipe == 0) {
				foreach (Element c in e.components) {
					total += c.recipe;
				}
				e.recipe = total;
			}
			recipeDict.Add (e.recipe, e);
			total = 0;
		}

	}
		
	public static Element Transmute(Element[] elementArray){
		HashSet<int> componentrecipes = new HashSet<int>();

		foreach (Element e in elementArray) {
			foreach (Element c in e.components) {
				componentrecipes.Add (c.recipe);
			}
		}

		return recipeDict [componentrecipes.Sum()];
	}

	public override string ToString()	{ return name; }
	#endregion

}
