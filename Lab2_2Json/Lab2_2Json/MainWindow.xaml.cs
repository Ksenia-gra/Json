using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2_2Json
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DataBaseFirstContext dbFirst { get; set; }
        public static SchoolDbContext dbSchool { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            dbFirst = new DataBaseFirstContext();
            dbFirst.Auths.Include(x => x.Books).Load();


            dbSchool = new SchoolDbContext();
            dbSchool.Students.Load();
            Stream webResponse = WebRequest.Create("https://restcountries.com/v3.1/capital/moscow").
                GetResponse().GetResponseStream();
            StreamReader streamReader = new StreamReader(webResponse, Encoding.UTF8);
            string deserJson = streamReader.ReadToEnd();
            deserializeText.Text = deserJson;
        }


        private void authJson_Click(object sender, RoutedEventArgs e)
        {

            string serialized = JsonConvert.SerializeObject(dbFirst.Auths.Select(x => new { x.Id, x.Name, x.Age, x.BooksStr }).ToList());
            serializeText.Text = serialized;
        }

        private void studentsJson_Click(object sender, RoutedEventArgs e)
        {
            string serialized = JsonConvert.SerializeObject(dbSchool.Students.ToList());
            serializeText.Text = serialized;
        }


        private void deserializeBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Rootobject rootobject = new Rootobject
            {Property1= JsonConvert.DeserializeObject<List<Class1>>(deserializeText.Text) };
            deserializeText.Text = null;

            
            foreach(Class1 class1 in rootobject.Property1)
            {
            
                deserializeText.Text+=class1.Print()+Environment.NewLine;
            }

        }
    }
}


