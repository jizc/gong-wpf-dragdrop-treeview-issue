namespace DragDropTreeViewIssue.ViewModels
{
    using System.Collections.ObjectModel;
    using MahApps.Metro.IconPacks;

    public class FolderViewModel : TreeViewModelBase
    {
        private PackIconMaterialKind icon = PackIconMaterialKind.Folder;
        private bool isExpanded = true;

        public FolderViewModel(string name)
            : base(name)
        {
        }

        public ObservableCollection<FileViewModel> Files { get; } = new ObservableCollection<FileViewModel>();

        public PackIconMaterialKind Icon
        {
            get => icon;
            private set => SetProperty(ref icon, value);
        }

        public override bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (SetProperty(ref isExpanded, value))
                {
                    Icon = value
                        ? PackIconMaterialKind.FolderOpen
                        : PackIconMaterialKind.Folder;
                }
            }
        }
    }
}
