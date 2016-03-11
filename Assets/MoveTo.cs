// MoveTo.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveTo : MonoBehaviour {

	public Transform goal;
	//private LineRe0nderer trail;
	public Dropdown destinations;
	private List<Vector3> pastPositions;
	private GameObject trailObject;
	private List<Dropdown.OptionData> destinationOptions;


	void Start () {
		
		destinationOptions = new List<Dropdown.OptionData> ();
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;

		trailObject = new GameObject ();
		trailObject.transform.position = this.gameObject.transform.position;

		//trail = trailObject.GetComponent<LineRenderer>();

		pastPositions = new List<Vector3> ();
		pastPositions.Add (trailObject.transform.position);

		//Debug.Log (pastPositions.Count);

		SetDestinationPositions ();

		destinations.onValueChanged.AddListener (delegate {
			destinationsDropdownValueChangedHandler(destinations);
		});
	}
	void Update()
	{

		NavMeshAgent nav = GetComponent<NavMeshAgent> ();
		if (nav == null || nav.path == null)
			return;

		LineRenderer line = this.GetComponent<LineRenderer> ();
		if (line == null) {
			line = this.gameObject.AddComponent<LineRenderer> ();
			//line.material = new Material( Shader.Find( "Sprites/Default" ) ) { color = Color.yellow };
			line.SetWidth (0.5f, 0.5f);
			line.SetColors (Color.yellow, Color.yellow);
		}



		NavMeshPath path = nav.path;
		//Debug.Log (path.corners.Length);
		line.SetVertexCount (path.corners.Length);

		for (int i = 0; i < path.corners.Length; i++) {
			line.SetPosition (i, path.corners [i]);
		}

		//trail Line
		//update trail gameobject position
		trailObject.transform.position = this.gameObject.transform.position;
		//Add position
		if (!(trailObject.transform.position == pastPositions.Last<Vector3> ())) {
			pastPositions.Add (trailObject.transform.position);
		}
		//LineRenderer
		LineRenderer trailLine = this.trailObject.GetComponent<LineRenderer> ();
		if (trailLine == null) {
			trailLine = trailObject.AddComponent<LineRenderer> ();
			trailLine.material = new Material (Shader.Find ("Sprites/Default")) { color = Color.yellow };
			trailLine.SetWidth (1f, 1f);
			trailLine.SetColors (Color.yellow, Color.yellow);
		}

		trailLine.SetVertexCount (pastPositions.Count);
		for (int i = 0; i < pastPositions.Count; i++) {
			trailLine.SetPosition (i, pastPositions [i]);
		}
  
	}

	void SetDestinationPositions( ){


		Dropdown.OptionData posA = new Dropdown.OptionData ("Edificio A");
		Dropdown.OptionData posB = new Dropdown.OptionData ("Edificio B");
		Dropdown.OptionData posC = new Dropdown.OptionData ("Edificio C");
		Dropdown.OptionData posD = new Dropdown.OptionData ("Edificio D");


		destinationOptions.Add (posA);
		destinationOptions.Add (posB);
		destinationOptions.Add (posC);
		destinationOptions.Add (posD);

		destinations.options = destinationOptions;
	}

	private void destinationsDropdownValueChangedHandler(Dropdown target){
		/*switch (target.value){
			case "Edificio A":
				break;
			case "Edificio B":
				break;
			case "Edificio C":
				break;
			case "Edificio D":
				break;
		}*/
	
	}
		
}