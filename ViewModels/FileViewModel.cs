namespace DragDropTreeViewIssue.ViewModels
{
    using JetBrains.Annotations;

    public class FileViewModel : TreeViewModelBase
    {
        public FileViewModel(string name, FolderViewModel folder)
            : base(name)
        {
            Folder = folder;
        }

        [NotNull]
        public FolderViewModel Folder { get; set; }

        public override bool CanAcceptChildren => false;
    }
}
