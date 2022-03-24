using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    public partial class FirstView : UserControl
    {
        public FirstView()
        {
            InitializeComponent();

            

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
