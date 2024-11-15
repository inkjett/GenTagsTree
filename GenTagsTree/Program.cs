using System.IO;
using System.Xml;
using System.Xml.Linq;
//#nullable disable

string fileName = "TestTree.xmlcfg";
string path = "C:\\Users\\administrator\\Documents\\GenTagsTree\\GenTagsTree\\bin\\Debug\\net8.0";
string fullPath = Path.Combine(path, fileName);
string FileData = "";

FileInfo opendFile = new FileInfo(fullPath);
if (!opendFile.Exists){
    Console.WriteLine("NoFile=" + fullPath);
    return; 
}
else{
    StreamReader ReadFile = new StreamReader(fullPath);
    FileData = ReadFile.ReadToEnd();
    ReadFile.Close();
}

XmlDocument xDoc = new XmlDocument();
xDoc.Load(fullPath);
XmlElement? xRoot = xDoc.DocumentElement;
foreach (XmlElement node in xRoot.ChildNodes)
{

    if (node.Name == "Signals")
    {
        //Console.WriteLine(node.Name);
        Console.WriteLine( GetData(node));
    }
}


static string  GetData(XmlNode _dataIn, string _path = "") // рекурсивная функция для вычитывания данных из xmlcfg
{
    string tmp = "";
    for (int i = 0; i < _dataIn.ChildNodes.Count; i++)
    {
        if (_dataIn.ChildNodes[i]?.Name == "Item")
        {
            //Console.WriteLine("Name1=" + tmp); 
            if (_dataIn.ChildNodes[i].Attributes.GetNamedItem("Type").Value == "Folder")
            {
                tmp += _dataIn.ChildNodes[i].Attributes.GetNamedItem("Name").Value;
            }
            if (_dataIn.ChildNodes[i].Attributes.GetNamedItem("Type").Value == "Double")
            {
                tmp += _dataIn.ChildNodes[i].Attributes.GetNamedItem("Name").Value + "\n";
            }

        }
        if (_dataIn.ChildNodes[i]?.HasChildNodes ?? false)
        {
             tmp += GetData(_dataIn.ChildNodes[i], tmp);
        }
    }
    return tmp;
}