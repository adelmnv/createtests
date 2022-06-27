using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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

namespace WPF_Study
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Test
    {
        public string Question { get; set; }
        public string Desc { get; set; }
        public string Points { get; set; }
        public List<(string,bool)> Answers { get; set; }
        public Test() { Answers = new List<(string, bool)>(); }

        public void ChangeAnswerstr(int index, string an)
        {
            Answers[index] = (an, Answers[index].Item2);
        }

        public void ChangeAnswerbool(int index, bool an)
        {
            Answers[index] = (Answers[index].Item1, an);
        }
    }

    public class Tests
    {
        ObservableCollection<Test> t = null;
        public Tests()
        {
            t = new ObservableCollection<Test>();
        }

        public ObservableCollection<Test> GetData()
        {
            return t;
        }
        public int Count()
        {
            return t.Count;
        }
        public Test ElementAt(int n)
        {
            return t[n];
        }

        public void Delete (int n)
        {
            t.RemoveAt(n);
        }

        public void AddElement(Test temp)
        {
            if(temp != null)
                t.Add(temp);
        }

    }
    public partial class MainWindow : Window
    {
        private const Visibility collapsed = Visibility.Collapsed;
        ViewModel viewModel;
        public Tests tests = new Tests();
        int number = 1;
        public bool On = true;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
        }

        public void AddTest()
        {
            int count = 0;
            Test t = new Test();
            (string, bool) tuple = ("", false);
            t.Points = Pointstbx.Text;
            foreach (var item in Questsp.Children.OfType<TextBox>())
            {
                if (count == 0)
                    t.Question = item.Text;
                else
                    t.Desc = item.Text;
                count++;
            }
            int iAn = 0;
            int iBt = 0;
            foreach (var itemA in Answsp.Children.OfType<TextBox>())
            {
                count = 0;
                iBt = 0;
                foreach (var item in Butsp.Children.OfType<CheckBox>())
                {
                    if (count == 0 && iAn==iBt)
                    {
                        bool check;
                        if (item.IsChecked == true)
                            check = true;
                        else
                            check = false;
                        tuple = (itemA.Text, check);
                        count++;
                    }
                    iBt++;
                    if (count > 0)
                        break;
                }
                if (tuple.Item1 != "")
                    t.Answers.Add(tuple);
                iAn++;
            }
            tests.AddElement(t);
        }

        public void ShowTest(int t)
        {
            int count = 0;
            Pointstbx.Text = tests.ElementAt(t).Points;
            foreach (var item in Questsp.Children.OfType<TextBox>())
            {
                if (count == 0)
                    item.Text = tests.ElementAt(t).Question;
                else
                    item.Text = tests.ElementAt(t).Desc;
                count++;
            }
            count = 0;
            foreach (var item in Answsp.Children.OfType<TextBox>())
            {
                string temp = tests.ElementAt(t).Answers.ElementAt(count).Item1;
                item.Text = temp;
                count++;
            }
            count = 0;
            foreach (var item in Butsp.Children.OfType<CheckBox>())
            {
                bool temp = tests.ElementAt(t).Answers.ElementAt(count).Item2;
                item.IsChecked = temp;
                count++;
            }
        }
        private void Save_click(object sender, MouseButtonEventArgs e)
        {
            if (ProperCheck())
            {
                if (tests.Count() < number)
                    AddTest();
            }
            string path = "test.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i =0; i < tests.Count(); i++)
                {
                    writer.WriteLine(tests.ElementAt(i).Question);
                    writer.WriteLine(tests.ElementAt(i).Desc);
                    for(int j =0; j < tests.ElementAt(i).Answers.Count(); j++)
                    {
                        writer.Write(tests.ElementAt(i).Answers[j].Item1);
                        writer.WriteLine("\t\t\t - {0}",tests.ElementAt(i).Answers[j].Item2);
                    }
                    writer.WriteLine(tests.ElementAt(i).Points);
                }
            }
            Close();
        }

        public void Clear()
        {
            foreach (var item in Questsp.Children.OfType<TextBox>())
                item.Text = "";
            foreach (var item in Answsp.Children.OfType<TextBox>())
                item.Text = "";
            foreach (var item in Butsp.Children.OfType<CheckBox>())
                item.IsChecked = false;
            Pointstbx.Text = "";
        }
        private void Clear_click(object sender, MouseButtonEventArgs e)
        {
            Clear();
        }

        private void Next_click(object sender, MouseButtonEventArgs e)
        {
            if (ProperCheck())
            {
                if (tests.Count() < number)
                    AddTest();
                else
                    CheckTest(number - 1);
                number++;
                numtbx.Text = number.ToString() + ".";
                if (tests.Count() >= number)
                    ShowTest(number - 1);
                else
                    Clear();
            }
        }

        public void CheckTest(int t)
        {
            if(Pointstbx.Text != tests.ElementAt(t).Points)
                tests.ElementAt(t).Points = Pointstbx.Text;
            int count = 0;
            foreach (var item in Questsp.Children.OfType<TextBox>())
            {
                if (count == 0)
                {
                    if (item.Text != tests.ElementAt(t).Question)
                        tests.ElementAt(t).Question = item.Text;
                }
                else
                {
                    if (item.Text != tests.ElementAt(t).Desc)
                        tests.ElementAt(t).Desc = item.Text;
                }
                count++;
            }
            count = 0;
            foreach (var item in Answsp.Children.OfType<TextBox>())
            {
                string temp = tests.ElementAt(t).Answers.ElementAt(count).Item1;
                if (item.Text != temp)
                    tests.ElementAt(t).ChangeAnswerstr(count, item.Text);
                count++;
            }
            count = 0;
            foreach (var item in Butsp.Children.OfType<CheckBox>())
            {
                bool temp = tests.ElementAt(t).Answers.ElementAt(count).Item2;
                if(item.IsChecked != temp)
                {
                    if(item.IsChecked == true)
                        tests.ElementAt(t).ChangeAnswerbool(count, true);
                    else
                        tests.ElementAt(t).ChangeAnswerbool(count, false);
                }
                count++;
            }
        }

        private void Previous_click(object sender, MouseButtonEventArgs e)
        {
            
            if (ProperCheck())
            {
                if (tests.Count() < number)
                    AddTest();
                else
                    CheckTest(number - 1);
            }
            if (number > 1)
            {
                number--;
                numtbx.Text = number.ToString() + ".";
                Clear();
                ShowTest(number - 1);
            }
        }

        public bool ProperCheck()
        {
            int count = 0;
            if (Pointstbx.Text == "")
                return false;
            foreach (var item in Questsp.Children.OfType<TextBox>())
            {
                if (item.Text == "")
                    return false;
            }
            foreach (var item in Answsp.Children.OfType<TextBox>())
            {
                if (item.Text == "")
                    return false;
            }
            foreach (var item in Butsp.Children.OfType<CheckBox>())
            {
                if (item.IsChecked == true)
                    count++;
            }
            if(count == 0)
                return false;
            return true;
        }

        private void Delete_click(object sender, MouseButtonEventArgs e)
        {
            string n = numtbx.Text.Trim('.');
            if (ProperCheck())
            {
                if (tests.Count() >= Int32.Parse(n))
                    tests.Delete(Int32.Parse(n) - 1);
            }
            if (tests.Count() > 0)
                ShowTest(tests.Count() - 1);
        }

        private void MultipleOn(object sender, RoutedEventArgs e)
        {
            On = true;
            foreach (var item in Butsp.Children.OfType<CheckBox>())
                item.IsEnabled = true;
        }

        private void MiltipleOff(object sender, RoutedEventArgs e)
        {
            On = false;
            int c = 0;
            foreach (var item in Butsp.Children.OfType<CheckBox>())
            {
                if (item.IsChecked == true)
                    c++;
            }
            if(c > 0)
            {
                foreach (var item in Butsp.Children.OfType<CheckBox>())
                {
                    if (item.IsChecked == false)
                        item.IsEnabled = false;
                }
            }
        }

        private void Ckecked_click(object sender, RoutedEventArgs e)
        {
            if (!On)
            {
                foreach (var item in Butsp.Children.OfType<CheckBox>())
                {
                    if(item.IsChecked == false)
                        item.IsEnabled = false;
                }
            }
        }
    }
}
