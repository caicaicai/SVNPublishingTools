using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SharpSvn;
using SharpSvn.Implementation;
using System.Collections.ObjectModel;


namespace SVNPublishingTools.ViewModel
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

        public ICommand fetch { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"

                fetch = new RelayCommand(() => fetchExec(), () => { return true; });
            }
        }

        private void fetchExec()
        {
            using (SvnClient client = new SvnClient())
            {
                Collection<SvnLogEventArgs> logItems = new Collection<SvnLogEventArgs>();
                client.GetLog(new Uri("http://192.168.1.193/svndata/fivestar/ecommerce/branches/dev2.1"), out logItems);

                Console.WriteLine(logItems[0].Author);

                SvnChangeItemCollection svnChange = logItems[0].ChangedPaths;

                Console.WriteLine(svnChange[0].Path);

                SvnTarget tgt = SvnTarget.FromString(svnChange[0].Path);

            }
        }
    }
}