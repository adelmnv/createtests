using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Study
{
    class ViewModel : INotifyPropertyChanged
    {
        //private Abstractions.IModel model;
        //private string textValue;
        //public string TextValue
        //{
        //    get
        //    {
        //        return textValue;
        //    }
        //    set
        //    {
        //        textValue = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public string Res { get; set; }
        //public ViewModel()
        //{
        //    model = new Model();
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged([CallerMemberName] string prop = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        //    if (prop == "bPlus_Click")
        //        model.SaveIn(TextValue);
        //    if (prop == "bEqual_Click")
        //    {
        //        model.SaveIn(TextValue);
        //        Res = model.Operation();
                
        //    }
         
        //    model.ShowIn(TextValue);
        //}
        
    }
}
