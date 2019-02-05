using System;
using System.Collections.Generic;
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

namespace MedPocketWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public TextBox sel_TxtBx; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CellClick(object sender, MouseButtonEventArgs e)
        {
            sel_TxtBx = (TextBox)sender;
            //sets value on Menus if already set prior[OPTIONAL]
        }

        private void m_Submit(object sender, RoutedEventArgs e)
        {
            string box_Text = "";
            //get value of dose type and timing
            //box_Text = ;
            //sel_TxtBx = box_Text;
            string cleanVal_t = StringRemover(Timing.SelectedItem.ToString(), Timing.SelectedItem.GetType().ToString());//removing reference to
            //assemblies 
            //TODO : SHORT LINE PLS
            string cleanVal_d = StringRemover(d_Type.SelectedItem.ToString(), d_Type.SelectedItem.GetType().ToString());
            //MessageBox.Show(cleanVal.Remove(0, 2));//removing : and a space
            string t_val = cleanVal_d + "\n" + cleanVal_t;
            sel_TxtBx.Text = t_val;
        }

        private void m_Aggr(object sender, RoutedEventArgs e)
        {
            int d_Small = 0;
            int d_Large = 0;
            MainWindow window = new MainWindow();
            foreach (TextBox t_box in FindVisualChildren<TextBox>(window))
            {
                //if(cur_Ctl.GetType() == typeof(TextBox))
                //{
                    //TextBox t_box = (TextBox)cur_Ctl;
                    if (t_box.Text.Contains("4S"))
                    {
                        d_Small += 4;
                    }
                    else if (t_box.Text.Contains("2S"))
                    {
                        d_Small += 2;
                        d_Large += 1;
                    }
                    else if (t_box.Text.Contains("2L"))
                    {
                        d_Large += 2;
                    }
                //}
                _aggr.Text = "Small: " + d_Small.ToString() + "\n Large: " + d_Large.ToString();
                MessageBox.Show(_aggr.Text);
            }
        }

        string StringRemover(string src, string rm)
        {
            int index = src.IndexOf(rm);
            string cleanPath = (index < 0)
                ? src
                : src.Remove(index, rm.Length);
            cleanPath = cleanPath.Remove(0, 2);
            return cleanPath;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject//TY STACK OVERFLOW :(
        {//WTAF IS A DEPENDENCY OBJECT BITCH
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
