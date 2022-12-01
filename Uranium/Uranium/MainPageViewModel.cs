using System.Collections.ObjectModel;
using System.Windows.Input;
using UraniumUI;

namespace Uranium
{
    public class MainPageViewModel : BindableObject
    {
        public ObservableCollection<TodoItem> Items { get; protected set; } = new ObservableCollection<TodoItem>();

        public ObservableCollection<TodoItem> SelectedItems { get; set; } = new ObservableCollection<TodoItem>();

        private TodoItem newItem = new();
        public TodoItem NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }

        public ICommand AddNewItemCommand { get; protected set; }

        public ICommand RemoveSelectedItemsCommand { get; protected set; }

        public MainPageViewModel()
        {
            if (Items.Count == 0)
            {
                Items.Add(new TodoItem { Content = "Throw away the rubbish", Type = TodoItem.TodoItemType.Personal });
                Items.Add(new TodoItem { Content = "Attend the meeting today\n11:00AM", Type = TodoItem.TodoItemType.Work });
                Items.Add(new TodoItem { Content = "Prepare presentation for new project", Type = TodoItem.TodoItemType.Work });
                Items.Add(new TodoItem { Content = "Spend time with family", Type = TodoItem.TodoItemType.Family });
                Items.Add(new TodoItem { Content = "Complete the puzzle", Type = TodoItem.TodoItemType.Hobby });
                Items.Add(new TodoItem { Content = "Don't forget to call dad", Type = TodoItem.TodoItemType.Family });
            }

            AddNewItemCommand = new Command(() =>
            {
                Items.Insert(0, NewItem);
                NewItem = new();
            });

            RemoveSelectedItemsCommand = new Command(() =>
            {
                foreach (var item in SelectedItems)
                {
                    Items.Remove(item);
                }
            });

            Nodes.Add(new MyItem("A")
            {
                Children =
            {
                new MyItem("A.1"),
                new MyItem("A.2"),
            }
            });
            Nodes.Add(new MyItem("B")
            {
                Children =
            {
                new MyItem("B.1")
                {
                    Children =
                    {
                        new MyItem("B.1.a"),
                        new MyItem("B.1.b"),
                        new MyItem("B.1.c"),
                        new MyItem("B.1.d"),

                    }
                },
                new MyItem("B.2"),
            }
            });
            Nodes.Add(new MyItem("C"));
            Nodes.Add(new MyItem("D"));
        }
    public ObservableCollection<MyItem> Nodes { get; set; } = new();
    }

    public class MyItem
    {
        public MyItem()
        {
        }

        public MyItem(string name) // For easy initialization (optional)
        {
            Name = name;
        }

        public virtual string Name { get; set; }
        public virtual IList<MyItem> Children { get; set; } = new ObservableCollection<MyItem>();
    }



    public class TodoItem : UraniumBindableObject
    {
        public string Content { get; set; }

        private bool isDone;
        public bool IsDone { get => isDone; set => SetProperty(ref isDone, value); }

        public TodoItemType Type { get; set; }

        public static TodoItemType[] AvailableTypes => Enum.GetValues(typeof(TodoItemType)) as TodoItemType[];

        public enum TodoItemType
        {
            Personal,
            Work,
            Hobby,
            Family
        }
    }
}