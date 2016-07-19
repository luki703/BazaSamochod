using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace BazaSamochod
{
    public partial class MainWindow : Window
    {
        List<Car> CarList = new List<Car>();
        List<Truck> TruckList = new List<Truck>();
        List<Motorcycle> MotorList = new List<Motorcycle>();
        List<string> FileLineList = new List<string>();
        string EndingMark = "+-+++";
        string Path= @"\fileData\";
        string FileName = "text.txt";
        public MainWindow()
        {
            InitializeComponent();
           // Decorator border = VisualTreeHelper.GetChild(ListView, 0) as Decorator;
          //  ScrollViewer scrollViewer = border.Child as ScrollViewer;
            OpenFile();
        }
        
        private void SelectVechicle()
        {
            VechicleForm.DataContext = ListView.SelectedItem;
        }
        private void ClearForm()
        {
            txtBrand.Clear();
            txtModel.Clear();
            txtYear.Clear();
            txtColor.Clear();
            cbCondition.SelectedIndex = 0;
            cbType.SelectedIndex = 0;
        }
        private void SaveToFile()
        {
            if (!Directory.Exists(Path))
            { Directory.CreateDirectory(Path); }
            string index = Guid.NewGuid().ToString();

            try
            {
                Condition state = (Condition)Enum.Parse(typeof(Condition), cbCondition.SelectedValue.ToString());

                if (cbType.SelectedValue.ToString() == Type.Samochód_Osobowy.ToString())
                {
                    CarList.Add(new Car(index, 
                                        txtBrand.Text, 
                                        txtModel.Text, 
                                        int.Parse(txtYear.Text), 
                                        state, 
                                        txtColor.Text,
                                        (Type)Enum.Parse(typeof(Type), cbType.SelectedValue.ToString())));
                    ListView.Items.Add(CarList[CarList.Count - 1]);
                }
                else if (cbType.SelectedValue.ToString() == Type.Motocykl.ToString())
                {
                    MotorList.Add(new Motorcycle(index, 
                                                 txtBrand.Text, 
                                                 txtModel.Text, 
                                                 int.Parse(txtYear.Text), 
                                                 state, 
                                                 txtColor.Text,
                                                 (Type)Enum.Parse(typeof(Type), cbType.SelectedValue.ToString())));
                    ListView.Items.Add(MotorList[MotorList.Count - 1]);
                }
                else if (cbType.SelectedValue.ToString() == Type.Ciężarówka.ToString())
                {
                    TruckList.Add( new Truck(index,
                                       txtBrand.Text,
                                       txtModel.Text,
                                       int.Parse(txtYear.Text),
                                       state,
                                       txtColor.Text,
                                       (Type)Enum.Parse(typeof(Type), cbType.SelectedValue.ToString())));
                    ListView.Items.Add(TruckList[TruckList.Count - 1]);
                }
                StreamWriter file = new StreamWriter(Path+FileName, true);
                file.WriteLine(index);
                file.WriteLine(cbType.SelectedValue);
                file.WriteLine(txtBrand.Text);
                file.WriteLine(txtModel.Text);
                file.WriteLine(txtYear.Text);
                file.WriteLine(txtColor.Text);
                file.WriteLine(cbCondition.SelectedValue.ToString());
                file.WriteLine(EndingMark);
                file.Close();
            }
            catch(FormatException)
            {
                MessageBox.Show("Prosze wprowadzić poprawne dane!");
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Prosze wprowadzić wszystkie dane!");
            }
        }

        private void OpenFile()
        {
            if (File.Exists(Path+FileName))
            {
                string[] tmp = new string[8];

                using (StreamReader reader = new StreamReader(Path + FileName))
                {
                    while (true)
                    {
                        for (int i = 0; i <= tmp.Length; i++)
                        {
                            tmp[i] = reader.ReadLine();

                            if (i == 7)
                            { break; }
                        }
                        if (tmp[7] == EndingMark & tmp[7] != null)

                        {if ((Type)Enum.Parse(typeof(Type), tmp[1]) == Type.Samochód_Osobowy)
                            {
                                    CarList.Add(new Car(tmp[0],
                                               tmp[2],
                                               tmp[3],
                                               int.Parse(tmp[4]),
                                               (Condition)Enum.Parse(typeof(Condition), tmp[6]),
                                               tmp[5],
                                               (Type)Enum.Parse(typeof(Type), tmp[1])));
                            }
                            else if ((Type)Enum.Parse(typeof(Type), tmp[1]) == Type.Motocykl)
                            {
                                MotorList.Add(new Motorcycle((tmp[0]),
                                                       tmp[2],
                                                       tmp[3],
                                                       int.Parse(tmp[4]),
                                                       (Condition)Enum.Parse(typeof(Condition),
                                                       tmp[6]),
                                                       tmp[5],
                                                       (Type)Enum.Parse(typeof(Type), tmp[1])));
                            }
                            else if ((Type)Enum.Parse(typeof(Type), tmp[1]) == Type.Ciężarówka)
                            {
                                TruckList.Add(new Truck(tmp[0],
                                                   tmp[2],
                                                   tmp[3],
                                                   int.Parse(tmp[4]),
                                                   (Condition)Enum.Parse(typeof(Condition),
                                                   tmp[6]),
                                                   tmp[5],
                                                   (Type)Enum.Parse(typeof(Type), tmp[1])));
                            }
                            else { }
                        }
                        else { break; }
                    }
                    FulfillListView();
                }
            }
            else { }
        }
        private void FulfillListView()
        {
           foreach (object obj in CarList)
            {
                ListView.Items.Add(obj);
            }
            foreach (object obj in TruckList)
            {
                ListView.Items.Add(obj);
            }
            foreach (object obj in MotorList)
            {
                ListView.Items.Add(obj);
            }
        }
        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            ListView.Items.Add(new Object());
            ClearForm();
        }
        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile();
            RefreshListView();
        }
        private void RefreshListView()
            {
            int counter = ListView.Items.Count;
            for (int i = 0; i<counter; i++)
            {
                ListView.Items.RemoveAt(0);
            }
            FulfillListView();
            }

        private void RewriteWholeFile()
        {
            try
            {
                CarList.RemoveAt(ListView.SelectedIndex);
            }
            catch ( ArgumentOutOfRangeException)
            {
                try
                {
                    TruckList.RemoveAt(ListView.SelectedIndex-CarList.Count);
                }
                catch (ArgumentOutOfRangeException)
                {
                    try
                    {
                        MotorList.RemoveAt(ListView.SelectedIndex-CarList.Count-TruckList.Count);
                    }
                    catch (ArgumentOutOfRangeException)
                    { }
                }
            }
            ListView.Items.Remove(ListView.SelectedItem);
            File.Delete(Path+FileName);
           
            foreach (Car obj in CarList )
            {
                StreamWriteFromClass(obj);
            }
            foreach (Truck obj in TruckList)
            {
                StreamWriteFromClass(obj);
            }
            foreach (Motorcycle obj in MotorList)
            {
                StreamWriteFromClass(obj);
            }
        }
        private void StreamWriteFromClass(Truck obj)
        {
            using (StreamWriter file = new StreamWriter(Path+FileName, true))
            {
                file.WriteLine(Guid.NewGuid().ToString());
                file.WriteLine(obj.Type.ToString());
                file.WriteLine(obj.Brand);
                file.WriteLine(obj.Model);
                file.WriteLine(obj.YearOfProduction);
                file.WriteLine(obj.Color);
                file.WriteLine(obj.Condition);
                file.WriteLine(EndingMark);
            }
        }
        private void StreamWriteFromClass(Car obj)
        {
            using (StreamWriter file = new StreamWriter(Path + FileName, true))
            {
                file.WriteLine(Guid.NewGuid().ToString());
                file.WriteLine(obj.Type.ToString());
                file.WriteLine(obj.Brand);
                file.WriteLine(obj.Model);
                file.WriteLine(obj.YearOfProduction);
                file.WriteLine(obj.Color);
                file.WriteLine(obj.Condition);
                file.WriteLine(EndingMark);
            }
        }
        private void StreamWriteFromClass(Motorcycle obj)
        {
            using (StreamWriter file = new StreamWriter(Path + FileName, true))
            {
                file.WriteLine(Guid.NewGuid().ToString());
                file.WriteLine(obj.Type.ToString());
                file.WriteLine(obj.Brand);
                file.WriteLine(obj.Model);
                file.WriteLine(obj.YearOfProduction);
                file.WriteLine(obj.Color);
                file.WriteLine(obj.Condition);
                file.WriteLine(EndingMark);
            }
        }
        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            RewriteWholeFile();
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
        private void ItemList_Checked(object sender, RoutedEventArgs e)
        {
            FulfillListView();
        }
        private void ItemList_UnChecked(object sender, RoutedEventArgs e)
        {
            ListView.Items.Clear();
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectVechicle();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile();
            RefreshListView();
        }
    }
}
