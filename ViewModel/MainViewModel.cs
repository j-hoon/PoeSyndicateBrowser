using GalaSoft.MvvmLight;

using PoeSyndicateBrowser.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace PoeSyndicateBrowser.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public TestModel TestModel { get => mTestModel; }

        public ICommand SaveTestModelCommand { get; private set; }

        private TestModel mTestModel;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            SaveTestModelCommand = new RelayCommand(SaveTestModelMethod);
            mTestModel = new TestModel(1, "ÀÌ¸§1");
        }


        public void SaveTestModelMethod()
        {
            this.RaisePropertyChanged(() => this.TestModel);

            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("TestModel saved."));
        }

    }
}