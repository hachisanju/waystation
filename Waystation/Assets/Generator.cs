using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Generator")]
public class Generator {

	public string[] Townspeople = new string[] {"Greg", "Dave",
	"Sarah", "Freddy", "Alex", "Ryan", "Jen"};
	

	[XmlAttribute("murderer")]
	public string Murderer;

	public void Generate() {
		int gen = Random.Range (0, 6);
		Murderer = Townspeople [gen];
	}

	public void Write(string path) {
		var serializer = new XmlSerializer (typeof(Generator));
		var stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}
}
