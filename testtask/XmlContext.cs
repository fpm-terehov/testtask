using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using testtask.Models;

namespace testtask
{
    class XmlContext
    {
        string path = "../../Students.xml";
        XDocument xdoc;
        public XElement Students { get; set; }
        XmlSerializer serializer = new XmlSerializer(typeof(Student));
        public List<Student> Load()
        {
            xdoc = XDocument.Load(path);
            Students = xdoc.Element("Students");
            Students stud;
            XmlSerializer seria = new XmlSerializer(typeof(Students));
            using (TextReader reader = new StringReader(Students.ToString()))
            {
                stud = (Students)seria.Deserialize(reader);
            }
            return stud.studs;
        }
        public void Save()
        {
            xdoc.Save(path);
        }
        public void Add(Student s)
        {
            using(var swr = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(swr))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, s,ns);
                var xe = XElement.Parse(swr.ToString());
                Students.Add(xe);
                Save();
            }
        }
        public void Remove(Student s)
        {
            var temp = from xe1 in Students.Elements("Student")
                       where xe1.Attribute("Id").Value == s.Id.ToString()
                       select xe1;
            XElement xe = temp.First();
            xe.Remove();
            Save();
        }
        public void Edit(Student s)
        {
            // получаем измененный объект
            var temp = from xe1 in Students.Elements("Student")
                       where xe1.Attribute("Id").Value == s.Id.ToString()
                       select xe1;
            var xe = temp.First();
            
            if (xe != null)
            {
                xe.Element("FirstName").Value = s.FirstName;
                xe.Element("Last").Value = s.Last;
                xe.Element("Age").Value = s.Age.ToString();
                xe.Element("Gender").Value = s.Gender.ToString();
                Save();
            }
        }
    }
}
