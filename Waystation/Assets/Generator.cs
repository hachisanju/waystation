using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Generator")]
public class Generator {

	[XmlAttribute("murderer")]
	public string Murderer;

	public void Write(string path) {
		var serializer = new XmlSerializer (typeof(Generator));
		var stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}
}
