using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace testtask.Models
{

    
    public class Student : INotifyPropertyChanged, IDataErrorInfo
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        string firstName = "";
        [XmlElement("FirstName")]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        string last = "";
        [XmlElement("Last")]
        public string Last
        {
            get
            {
                return last;
            }
            set
            {
                last = value;
                OnPropertyChanged("Last");
            }
        }
        string age = "";
        [XmlElement("Age")]
        public string Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }
        int gender;

        [XmlElement("Gender")]
        public int Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        int v = 0;
                        int.TryParse(Age,out v);
                        if ((v < 16 || v > 100) && Age!="")
                        {
                            error = "Возраст должен быть больше 15 и меньше 101";
                        }
                        break;
                    case "FirstName":
                        if(FirstName =="")
                        {
                            error = "Обязательно для заполнения";
                        }
                        break;
                    case "Last":
                        if (Last == "")
                        {
                            error = "Обязательно для заполнения";
                        }
                        break;
                }
                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
