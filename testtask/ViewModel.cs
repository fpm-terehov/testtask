using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using testtask.Models;

namespace testtask
{
    public class ViewModel : INotifyPropertyChanged
    {
        XmlContext xdoc;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        List<Student> students;

        public List<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }

        public ViewModel()
        {
            xdoc = new XmlContext();
            Students = xdoc.Load();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      StudentWindow studentWindow = new StudentWindow(new Student());
                      if (studentWindow.ShowDialog() == true)
                      {
                          Student Student = studentWindow.Student;
                          if (students.Count == 0)
                              Student.Id = 0;
                          else
                              Student.Id = students.Last().Id + 1;
                          xdoc.Add(Student);
                          Students = xdoc.Load();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItems) =>
                  {
                      // получаем выделенный объект
                      System.Collections.IList  lst = (System.Collections.IList)selectedItems;
                      var lit = lst.Cast<Student>();
                      if (lit.Count() != 1)
                      {
                          MessageBox.Show("Выберите одного студента для редактирования.");
                          return;
                      }
                      Student Student = lit.First();

                      Student vm = new Student()
                      {
                          Id = Student.Id,
                          FirstName = Student.FirstName,
                          Last = Student.Last,
                          Age = Student.Age,
                          Gender = Student.Gender
                      };
                      StudentWindow StudentWindow = new StudentWindow(vm);


                      if (StudentWindow.ShowDialog() == true)
                      {
                          xdoc.Edit(StudentWindow.Student);
                          Students = xdoc.Load();                          
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItems) =>
                  {
                      System.Collections.IList lst = (System.Collections.IList)selectedItems;
                      var lit = lst.Cast<Student>();
                      string message;
                      if (lit.Count() < 1) return;
                      if(lit.Count() == 1)
                          message = "Вы действительно хотите удалить выбранный элемент?";
                      else
                          message = "Вы действительно хотите удалить выбранные элементы?";
                      // получаем выделенный объект
                      if (MessageBox.Show(message, "delete", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                          return;
                      foreach (Student Student in lit)
                      {
                          xdoc.Remove(Student);
                      }          
                      xdoc.Save();
                      Students = xdoc.Load();
                  }));
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
