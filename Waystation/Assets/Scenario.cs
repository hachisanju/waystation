using UnityEngine;
using System.Collections;
using System.IO;

public class Scenario : MonoBehaviour {

	public Generator scenarioGenerator = new Generator();

	// Use this for initialization
	void Start () {
		scenarioGenerator.Generate ();
		scenarioGenerator.Write (Path.Combine (Application.persistentDataPath, "save1.xml"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
