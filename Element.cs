using UnityEngine;
using mono = UnityEngine.MonoBehaviour;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public sealed class Element : IComparable<Element>{

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

	public static readonly Element[] available = new Element[] {
		aether, air, fire, water,
		dark, ice, light, metal, spark,
		dragon, mech, megapoda, spirit	};
	
	private string namef;
	private Element[] componentsf;

	public string name {
		get 		{ return namef; }
		private set { namef = value; }
	}

	public Element[] components{
		get 		{ return componentsf; }
		private set { componentsf = value; }
	}

	private static Dictionary<string, Element> recipeDict;

	private string recipe;

	private static readonly Element init = new Element ("Init"); //for initialization purposes

	private Element(string str){
		if (str == "Init") {
			TextAsset eRecipesCSV = GameObject.FindObjectOfType<Transmutor> ().eRecipesCSV;
			Init (eRecipesCSV);
		} else {
			name = str;
		}
	}
		
	private static void Init(TextAsset textAsset){
		//For ordering strings, needed a couple of times.
		List<string> strList;
		List<string> stringList = new List<string> ();
		List<string> components;
		List<char> sortingList;

		//Sorting in the hopes that it might increase performance? Don't think so, but it's the same
		elementStrings.Sort();

		//Fills out the name and recipe fields of each element based on eRecipes.csv
		string text = textAsset.ToString ();
		string[] array = text.Split('*');
		List<Element> componentsAsElements = new List<Element> ();
		foreach (string str in array) {
			strList = str.Split(',').ToList();
			components = strList.GetRange (1, strList.Count - 1);
			componentsAsElements.Clear ();
			foreach (string comp in components) {
				mono.print (comp);
				componentsAsElements.Add ((Element)comp);
				mono.print ("Comp as Element: " + (Element)comp);
			}
			foreach (Element e in elements) {
				if (e.name.Equals (strList [0])) {
					e.components = componentsAsElements.ToArray();

					string recipe = GiveRecipe (e.components);
					if (recipe.Contains ("S")) {
						sortingList = recipe.ToList ();
						sortingList.Sort ();
						stringList.Clear ();
						foreach (char c in sortingList) {
							stringList.Add (c.ToString ());
						}
						e.recipe = string.Join ("", stringList.ToArray ());
					} else {
						e.recipe = recipe;
					}
					break;
				}
			}
		}

		//Needed for next step, precalculated here. S = Aether.
		string[] allPermutationsWORep = new string[] {"S", "A", "E", "F", "W",
			"SA", "SE", "SF", "SW", "AS", "AE", "AF", "AW", "ES", "EA", "EF",
			"EW", "FS", "FA", "FE", "FW", "WS", "WA", "WE", "WF", "SAE", "SAF",
			"SAW", "SEA", "SEF", "SEW", "SFA", "SFE", "SFW", "SWA", "SWE", "SWF",
			"ASE", "ASF", "ASW", "AES", "AEF", "AEW", "AFS", "AFE", "AFW", "AWS",
			"AWE", "AWF", "ESA", "ESF", "ESW", "EAS", "EAF", "EAW", "EFS", "EFA",
			"EFW", "EWS", "EWA", "EWF", "FSA", "FSE", "FSW", "FAS", "FAE", "FAW",
			"FES", "FEA", "FEW", "FWS", "FWA", "FWE", "WSA", "WSE", "WSF", "WAS",
			"WAE", "WAF", "WES", "WEA", "WEF", "WFS", "WFA", "WFE", "SAEF", "SAEW",
			"SAFE", "SAFW", "SAWE", "SAWF", "SEAF", "SEAW", "SEFA", "SEFW", "SEWA",
			"SEWF", "SFAE", "SFAW", "SFEA", "SFEW", "SFWA", "SFWE", "SWAE", "SWAF",
			"SWEA", "SWEF", "SWFA", "SWFE", "ASEF", "ASEW", "ASFE", "ASFW", "ASWE",
			"ASWF", "AESF", "AESW", "AEFS", "AEFW", "AEWS", "AEWF", "AFSE", "AFSW",
			"AFES", "AFEW", "AFWS", "AFWE", "AWSE", "AWSF", "AWES", "AWEF", "AWFS",
			"AWFE", "ESAF", "ESAW", "ESFA", "ESFW", "ESWA", "ESWF", "EASF", "EASW",
			"EAFS", "EAFW", "EAWS", "EAWF", "EFSA", "EFSW", "EFAS", "EFAW", "EFWS",
			"EFWA", "EWSA", "EWSF", "EWAS", "EWAF", "EWFS", "EWFA", "FSAE", "FSAW",
			"FSEA", "FSEW", "FSWA", "FSWE", "FASE", "FASW", "FAES", "FAEW", "FAWS",
			"FAWE", "FESA", "FESW", "FEAS", "FEAW", "FEWS", "FEWA", "FWSA", "FWSE",
			"FWAS", "FWAE", "FWES", "FWEA", "WSAE", "WSAF", "WSEA", "WSEF", "WSFA",
			"WSFE", "WASE", "WASF", "WAES", "WAEF", "WAFS", "WAFE", "WESA", "WESF",
			"WEAS", "WEAF", "WEFS", "WEFA", "WFSA", "WFSE", "WFAS", "WFAE", "WFES",
			"WFEA", "SAEFW", "SAEWF", "SAFEW", "SAFWE", "SAWEF", "SAWFE", "SEAFW",
			"SEAWF", "SEFAW", "SEFWA", "SEWAF", "SEWFA", "SFAEW", "SFAWE", "SFEAW",
			"SFEWA", "SFWAE", "SFWEA", "SWAEF", "SWAFE", "SWEAF", "SWEFA", "SWFAE",
			"SWFEA", "ASEFW", "ASEWF", "ASFEW", "ASFWE", "ASWEF", "ASWFE", "AESFW",
			"AESWF", "AEFSW", "AEFWS", "AEWSF", "AEWFS", "AFSEW", "AFSWE", "AFESW",
			"AFEWS", "AFWSE", "AFWES", "AWSEF", "AWSFE", "AWESF", "AWEFS", "AWFSE",
			"AWFES", "ESAFW", "ESAWF", "ESFAW", "ESFWA", "ESWAF", "ESWFA", "EASFW",
			"EASWF", "EAFSW", "EAFWS", "EAWSF", "EAWFS", "EFSAW", "EFSWA", "EFASW",
			"EFAWS", "EFWSA", "EFWAS", "EWSAF", "EWSFA", "EWASF", "EWAFS", "EWFSA",
			"EWFAS", "FSAEW", "FSAWE", "FSEAW", "FSEWA", "FSWAE", "FSWEA", "FASEW",
			"FASWE", "FAESW", "FAEWS", "FAWSE", "FAWES", "FESAW", "FESWA", "FEASW",
			"FEAWS", "FEWSA", "FEWAS", "FWSAE", "FWSEA", "FWASE", "FWAES", "FWESA",
			"FWEAS", "WSAEF", "WSAFE", "WSEAF", "WSEFA", "WSFAE", "WSFEA", "WASEF",
			"WASFE", "WAESF", "WAEFS", "WAFSE", "WAFES", "WESAF", "WESFA", "WEASF",
			"WEAFS", "WEFSA", "WEFAS", "WFSAE", "WFSEA", "WFASE", "WFAES", "WFESA", "WFEAS" };

		//Initializes Dictionary<string, Element> recipeDict
		recipeDict = new Dictionary<string, Element> ();
		string sortedString;
		foreach (string r in allPermutationsWORep) { //Note: would like to turn it into a pickle at some point
			Element target = null;
			sortingList = r.ToList ();
			sortingList.Sort ();
			stringList.Clear();
			foreach (char c in sortingList) {
				stringList.Add (c.ToString());
			}
			sortedString = string.Join ("", stringList.ToArray());
			foreach (Element e in elements) {
				if (e.recipe.Equals (sortedString)) {
					target = e;
					break;
				}
			}
			recipeDict.Add(r, target);
		}

	}

	#region Utility shit
	//what do I want? I want...
	//One element object for each element, containing the string name, components as an Element[], and the recipe as a string
	//since that is 1) useful for other initialization steps, and 2) maybe better than the components in some case
	//as well as the ability to give a recipe and return the element result
	//Translates to:
	//Element.sound.name -> "Sound" (for image setting)
	//Element.sound.components -> [Element.aether, Element.air] (for reversing transmutation & checking if valid for mon)
	//Element.aether + Element.air -> Element.sound (for combining elements) //adding x to x enhances to a point (eventually)
	//adding components can't work, but needs code response, i.e. can't return null or the superset element
	//Element.sound - Element.air -> Element.sound (for combining elements) //don't actually think we need this

	public override string ToString()	{ return name; }

	public static explicit operator Element(string str){
		//TODO: Turn elements into a HashSet for faster casting
		for (int i = 0; i < elementStrings.Count; i++){
			mono.print ("Str: " + str);
			mono.print ("ElementStrings[i]" + elementStrings [i]);
			mono.print ("Elements[i]" + elements [i]);
			if (string.Equals(str, elementStrings[i], System.StringComparison.OrdinalIgnoreCase)){
//				mono.print ("Str: " + str);
//				mono.print ("ElementStrings[i]" + elementStrings [i]);
//				mono.print ("Elements[i]" + elements [i]);
				return elements[i];
			}
		}
		UnityEngine.Debug.Log ("Error: \"" + str + "\" is not the name of an element, Origin: Element cast from string");
		return null; //Doesn't this possibility mean that this cast should be explicit?
	}

	public static implicit operator string(Element e){ return e.name; }

	public int CompareTo(Element that){
		return this.name.CompareTo (that.name);
	}

	public static Element operator +(Element former, Element latter)
	{
		int comparison = former.CompareTo (latter);
		if (comparison == 0) {
			return former;
		}

		IEnumerable<char> iec = (former.recipe + latter.recipe).Distinct ();
		mono.print (former.recipe);
		List<string> stringList = new List<string>();
		foreach (char c in iec) {
			stringList.Add(c.ToString());
			mono.print (c.ToString ());
		}
		string r = string.Join ("", stringList.ToArray());
		mono.print ("recipe: " + r);
		return recipeDict [r];

	}

//	private static string DecideLetter(Element e, List<Element> l, string s){
//		if (l.Contains (e)) {
//			return s + e.name [0];
//		}
//		DecideLetter(e
//	}

	private static string GiveRecipe(Element[] elementArray){
		string recipe = "";
		foreach (Element e in elementArray) {
//			mono.print ("GiveRecipe for " + e.ToString ());
//			mono.print ("Component1" + e.components[0].ToString ());
			foreach (Element c in e.components) {
				if (c == aether) {
					recipe += "S";
				} else {
					recipe += c.name [0];
				}
			}
		}
		return new string (recipe.Distinct ().ToArray ());
	}

	public static Element Transmute(Element[] elementArray){
		string recipe = GiveRecipe (elementArray);
		IEnumerable<char> iec = recipe.Distinct ();
		List<string> stringList = new List<string>();
		foreach (char c in iec) {
			stringList.Add(c.ToString());
		}
		recipe = string.Join ("", stringList.ToArray());
		return recipeDict [recipe];
	}

	/// <summary>
	/// Convenience method for dev purposes. Faster, but requires you to do some work.
	/// Don't use for production purposes.
	/// Instructions: For each element, consider its components, e.g. dragon = air + earth + fire.
	/// Take the first letter of each, capitalize it, and turn it into a string (dragon = AEF)
	/// Order does not matter and Aether is "S" (e.g. sound = "SA" or "AS", both work).
	/// Finally, remove any duplicates.
	/// Example: steam + basilisk + air -> F + W + A + E + S + A -> FWAESA -> FWAES = Sidereal...
	/// Summary: Take first letter of name for each component, Aether is "S", no dupe characters, order not important
	/// </summary>
	public static Element Transmute(string recipe){
		return recipeDict [recipe]; 
	}

	//thoughts
	//so each element must have a field of its recipe, no? So that when combining say sound + dark
	//it goes foreach element in sound.components, give recipe, then add those to dark's, then remove duplicates
	//then plug into dictionary. Sorting not required, I've decided to just have more keys, since the number of values
	//in memory is still the same. A hundred or more extra strings and their connections in the dictionary don't seem
	//more valuable than the processing time of sorting over and over again many dozens of times. But that's just me.
	//UPDATE: Learned what sorting strings is like in c#. Yes. Definitely. Once all the element names are finalized,
	//pickle the damn dictionary, remove calls to Init, keep the function just in case, and hope that's it. It's just
	//too bad that I still have to do the distinct character nonsense in GiveRecipe, but what can I do? If there's a
	//better, faster, and cleaner looking way to do this that doesn't involve strings, I'm all for it. But right now,
	//everything I've found is way above my paygrade. I don't have to time to learn it, unfortunately, so hopefully one
	//day I'll know more.

	#endregion
}