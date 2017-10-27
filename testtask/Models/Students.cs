using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace testtask.Models
{
    [XmlRoot("Students")]
    public class Students
    {
        [XmlElement("Student")]
        public List<Student> studs { get; set; }
    }
}
