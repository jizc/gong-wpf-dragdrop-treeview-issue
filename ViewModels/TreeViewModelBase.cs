namespace DragDropTreeViewIssue.ViewModels
{
    public abstract class TreeViewModelBase : ViewModelBase
    {
        private bool isSelected;

        protected TreeViewModelBase(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        public virtual bool IsExpanded { get; set; }
        public virtual bool CanAcceptChildren => true;
    }
}
