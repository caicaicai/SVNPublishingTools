using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SharpSvn;
using SharpSvn.Implementation;
using System.Data;

namespace SVNPublishingTools.Model
{
    public class SVN : INotifyPropertyChanged
    {
        private Collection<SvnLogEventArgs> svnLogs;

        public Collection<SvnLogEventArgs> SvnLogs
        {
            get { return svnLogs; }
            set
            {
                svnLogs = value;
                RaisePropertyChanged("SvnLogs");
                ConvertSvnLog();
            }
        }

        private DataTable svnLogDataTable;

        public DataTable SvnLogDataTable
        {
            get { return svnLogDataTable; }
            set
            {
                svnLogDataTable = value;
                RaisePropertyChanged("SvnLogDataTable");
            }
        }

        public SVN()
        {
            SvnLogDataTable = new DataTable();
            SvnLogDataTable.Columns.Add(new DataColumn("Revision", typeof(string)));
            SvnLogDataTable.Columns.Add(new DataColumn("Actions", typeof(string)));
            SvnLogDataTable.Columns.Add(new DataColumn("Author", typeof(string)));
            SvnLogDataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            SvnLogDataTable.Columns.Add(new DataColumn("Message", typeof(string)));
        }

        public void ConvertSvnLog()
        {
            foreach(var svnlog in SvnLogs)
            {
                DataRow row1 = SvnLogDataTable.NewRow();
                //行赋值
                row1["Revision"] = svnlog.Revision;
                row1["Actions"] = "action";
                row1["Author"] = svnlog.Author;
                row1["Date"] = svnlog.Time;
                row1["Message"] = svnlog.LogMessage;
                //添加行
                SvnLogDataTable.Rows.Add(row1);
            }
        }



        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
