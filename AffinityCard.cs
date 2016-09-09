public enum Affinity { Slash, Sphere };

public class AffinityCard : Card {
	
	#region AFFINITYCARD:FIELDS
	//Note: All fields have properties, and vice-versa. Field names end with an F.
	private Affinity affinityf;
	private int idf;
	private int powerf;
	#endregion

	#region AFFINITYCARD:PROPERTIES
	public Affinity affinity {
		get { return affinityf; }
		private set { affinityf = value; } }
	#endregion

	#region AFFINITYCARD:CONSTRUCTORS
	public AffinityCard(Affinity a){
		affinityf = a;
	}
	#endregion

	#region AFFINITYCARD:OVERRIDDEN-MEMBERS
	public override int 	id {
		get { return idf; }
	}
	public override string 	imageName {
		get { return "null";	}
	}
	public override int		power {
		get { return 0;	}
	}
	#endregion

}