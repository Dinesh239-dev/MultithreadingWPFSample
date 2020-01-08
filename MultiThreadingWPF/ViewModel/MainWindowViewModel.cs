namespace MultiThreadingWPF
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;
    using MultiThreadingWPF.Model;

    public class MainWindowViewModel : BindableBase
    {

        private static Queue<Action> queue = new Queue<Action>();
        private static readonly object queuelock = new object();
        private string printNumbers;
        private string taskName;
        private static int taskNumber = 1;
        private TaskModel __taskModel;
        private string executionOrder;
        private bool isOddChecked;
        private bool isEvenChecked;
        private bool _isSelected;
        public ICommand StartButtonCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }

        public TaskModel TasksModel
        {
            get { return __taskModel; }
            set { SetProperty(ref __taskModel, value); RaisePropertyChanged(); }
        }
        
        public string ExecutionOrder
        {
            get { return executionOrder; }
            set { SetProperty(ref executionOrder, value); RaisePropertyChanged(); }
        }
        
        public bool IsOddChecked
        {
            get { return isOddChecked; }
            set { SetProperty(ref isOddChecked, value); RaisePropertyChanged(); }
        }
        
        public bool IsEvenChecked
        {
            get { return isEvenChecked; }
            set { SetProperty(ref isEvenChecked, value); RaisePropertyChanged(); }
        }
        
        public string TaskName
        {
            get { return taskName; }
            set
            {
                taskName = value;
                RaisePropertyChanged();
            }
        }
        public string PrintNumbers
        {
            get { return printNumbers; }
            set
            {
                printNumbers = value;
                RaisePropertyChanged();
            }
        }
        
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; RaisePropertyChanged(); }
        }
        

        public MainWindowViewModel()
        {
            TasksModel = new TaskModel(); 
            StartButtonCommand = new DelegateCommand(TaskExecutionOrder);
            AddTaskCommand = new DelegateCommand(SelectedTaskExecution);
            Task.Run(() => ExecuteTasks());
        }
      
        private void SelectedTaskExecution()
        {
            if(isEvenChecked)
            {
                EvenNumbers();
            }
            else if(isOddChecked)
            {
                OddNumbers();
            }
        }

        public void TaskExecutionOrder()
        {
            queue.Enqueue(OddNumbers);
            queue.Enqueue(EvenNumbers);
            
        }

        private async void ExecuteTasks()
        {
            printNumbers = "Service Started - Q Count: " + queue.Count;
            while (true)
            {
                Action task = null;
                lock (queuelock)
                {
                    if (queue.Count > 0)
                    {
                        task = queue.Dequeue();
                    }
                }
                if (task != null)
                {
                    await Task.Run(() =>
                    {
                        task.Invoke();
                        Thread.Sleep(100);
                    });
                }

            }
        }

        private void EvenNumbers()
        {
                 Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    this.PrintNumbers += Environment.NewLine;
                    this.PrintNumbers += Environment.NewLine;
                    this.PrintNumbers += "EvenNumbers between 1 to 100" + Environment.NewLine + "Task:" + " " + taskNumber.ToString();
                    this.PrintNumbers += Environment.NewLine;
                    this.ExecutionOrder += "Task:" + taskNumber.ToString() + "-Started " + "\n";

                });

            for (int i = 0; i <= 100; i++)
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    if (i % 2 == 0)
                    {
                        this.PrintNumbers += i.ToString() + " ";
                        Thread.Sleep(10);
                    }
                });
            }

            this.ExecutionOrder += "Task:" + taskNumber.ToString() + "-Stopped" + "\n";
            taskNumber++;
            Thread.Sleep(1000);
        }
        private void OddNumbers()
        {
                Dispatcher.CurrentDispatcher.Invoke(() =>
               {
                   this.PrintNumbers += Environment.NewLine;
                   this.PrintNumbers += Environment.NewLine;
                   this.PrintNumbers += "OddNumbers between 1 to 100" + Environment.NewLine + "Task:" + " " + taskNumber.ToString();
                   this.PrintNumbers += Environment.NewLine;
                   this.ExecutionOrder += "Task:" + taskNumber.ToString() + "-Started" + "\n";

               });
                for (int i = 0; i <= 100; i++)
                {

                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        if (i % 2 != 0)
                        {
                            this.PrintNumbers += i.ToString() + " ";

                            Thread.Sleep(10);
                        }

                    });

                }
                
                this.ExecutionOrder += "Task:" + taskNumber.ToString() + "-Stopped" + "\n";
                taskNumber++;
                Thread.Sleep(1000);
            
        }
       
    }
}