using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        bool isEditingExisting = false;
        string title = "";
        string description = "";
        ToDo current = null;

        public String Title
        {
            get { return title; }
            set
            {
                this.RaiseAndSetIfChanged(ref title, value);
            }
        }
        public String Description
        {
            get { return description; }
            set
            {
                this.RaiseAndSetIfChanged(ref description, value);
            }
        }

        DateTimeOffset date = DateTimeOffset.Now.Date;
        public DateTimeOffset Date
        {
            set
            {
                this.RaiseAndSetIfChanged(ref date, value);
                this.ChangeObservableCollection(this.date);
            }
            get
            {
                return this.date;
            }
        }
        public ObservableCollection<ToDo> Items { get; set; }



        private Dictionary<DateTimeOffset, List<ToDo>> ListsOnDays;

        ViewModelBase content;
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public MainWindowViewModel()
        {
            this.ListsOnDays = new Dictionary<DateTimeOffset, List<ToDo>>();
            this.Items = new ObservableCollection<ToDo>();
            this.Content = new FirstViewModel();
        }

        private void InitToDoList()
        {
            var ListsOnDays = new Dictionary<DateTimeOffset, List<ToDo>>();
            ListsOnDays.Add(this.date, new List<ToDo>());
            this.ListsOnDays = ListsOnDays;
        }

        public void AppendAction(DateTimeOffset date, ToDo item)
        {
            if (!this.ListsOnDays.ContainsKey(date))
            {
                this.ListsOnDays.Add(date, new List<ToDo>());
            }
            this.ListsOnDays[date].Add(item);
            this.ChangeObservableCollection(this.Date);
        }


        public void ChangeView()
        {
            if (this.Content is FirstViewModel)
            {
                this.Content = new SecondViewModel();

            }
            else
            {
                this.Title = "";
                this.Description = "";
                this.current = null;
                this.isEditingExisting = false;
                this.Content = new FirstViewModel();
            }
        }

        public void ChangeObservableCollection(DateTimeOffset date)
        {
            if (!this.ListsOnDays.ContainsKey(date))
            {
                this.Items.Clear();
            }
            else
            {
                this.Items.Clear();
                foreach (var item in this.ListsOnDays[date])
                {
                    this.Items.Add(item);
                }
            }
        }

        public void SaveChanges()
        {
            if (this.Title != "")
            {
                if (this.isEditingExisting)
                {
                    var item = this.ListsOnDays[date].Find(x => x.Equals(this.current));
                    item.Title = this.Title;
                    item.Description = this.Description;
                    this.isEditingExisting = false;
                }
                else
                {
                    this.AppendAction(this.Date, new ToDo(this.Title, this.Description));
                }
                this.ChangeView();
            }
        }

        public void DeleteItem(ToDo item)
        {
            this.ListsOnDays[date].Remove(item);
            this.ChangeObservableCollection(date);
        }

        public void ViewExisting(ToDo item)
        {
            this.isEditingExisting = true;
            this.current = item;
            this.Title = current.Title;
            this.Description = current.Description;
            this.ChangeView();
        }

    }
}
