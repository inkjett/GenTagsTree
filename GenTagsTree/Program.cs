using System.IO;
using System.Xml;
using System.Xml.Linq;

string fileName = "TestTree.xmlcfg";
string path = "C:\\Users\\AutomiqUsr\\Documents\\GenTagsTree\\GenTagsTree\\bin\\Debug\\net8.0";
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
        GetData(node);
    }
}


static void GetData(XmlElement _dataIn) // рекурсивная функция для вычитывания данных из xmlcfg
{
    if (_dataIn.Name == "Item")
    {
        if (_dataIn.Attributes.GetNamedItem("Type").Value == "Folder")
        {
            Console.WriteLine("Folder=" + _dataIn.Attributes.GetNamedItem("Name").Value);
        }
        else if (_dataIn.Attributes.GetNamedItem("Type").Value == "Double")
        {
            Console.WriteLine("Double=" + _dataIn.Attributes.GetNamedItem("Name").Value);
        }
        else if (_dataIn.Attributes.GetNamedItem("Type").Value == "Bool")
        {
            Console.WriteLine("Bool=" + _dataIn.Attributes.GetNamedItem("Name").Value);
        }
        else if (_dataIn.Attributes.GetNamedItem("Type").Value == "Float")
        {
            Console.WriteLine("Float=" + _dataIn.Attributes.GetNamedItem("Name").Value);
        }
    }
    if (_dataIn.HasChildNodes)
    {
        foreach (XmlElement childnode in _dataIn.ChildNodes)
        {
            GetData(childnode);
        }
    }
}