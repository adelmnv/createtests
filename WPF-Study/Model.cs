using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WPF_Study
{
    class Model : Abstractions.IModel, INotifyPropertyChanged
    {
        //public int firstValue;
        //public int secondValue;
        //public int result;

        public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged([CallerMemberName] string prop = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        //}
        //public void ShowIn(string message)
        //{
        //    Debug.WriteLine(message);
        //    OnPropertyChanged();
        //}
        //public void SaveIn(string message)
        //{
        //    Debug.WriteLine(message);
        //    if (firstValue == 0)
        //        firstValue = Int32.Parse(message);
        //    else
        //        secondValue = Int32.Parse(message);
        //    OnPropertyChanged();
        //}
        //public string Operation()
        //{
        //    result = firstValue + secondValue;
        //    return result.ToString();
        //}

    }
}
