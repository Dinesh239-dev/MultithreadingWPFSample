using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadingWPF.Model 
{
    public class TaskModel : INotifyPropertyChanged
    {
        private string TaskName { get; set; }
        private string PrintNumbers { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
