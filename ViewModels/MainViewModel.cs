namespace DragDropTreeViewIssue.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using GongSolutions.Wpf.DragDrop;

    public class MainViewModel : ViewModelBase, IDropTarget
    {
        public MainViewModel()
        {
            var fileIndex = 1;
            for (var i = 1; i < 3; ++i)
            {
                var folder = new FolderViewModel($"Folder {i}");
                for (var j = 1; j < 6; ++j)
                {
                    folder.Files.Add(new FileViewModel($"File {fileIndex++}", folder));
                }

                Folders.Add(folder);
            }
        }

        public ObservableCollection<FolderViewModel> Folders { get; } = new ObservableCollection<FolderViewModel>();

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            if (ReferenceEquals(dropInfo.Data, dropInfo.TargetItem)
                || !(dropInfo.Data is FileViewModel file))
            {
                return;
            }

            if (dropInfo.TargetItem is FolderViewModel folder && !ReferenceEquals(file.Folder, folder))
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            }
            else if (dropInfo.TargetItem is FileViewModel)
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (ReferenceEquals(dropInfo.Data, dropInfo.TargetItem)
                || !(dropInfo.Data is FileViewModel file))
            {
                return;
            }

            var insertIndex = dropInfo.InsertIndex;
            FolderViewModel target;
            switch (dropInfo.TargetItem)
            {
                case FolderViewModel folder:
                    target = folder;
                    insertIndex = -1;
                    break;
                case FileViewModel f:
                    target = f.Folder;
                    if (ReferenceEquals(target, file.Folder) && dropInfo.DragInfo.SourceIndex < dropInfo.InsertIndex)
                    {
                        insertIndex--;
                    }

                    break;
                default:
                    return;
            }

            file.Folder.Files.Remove(file);
            file.Folder = target;
            if (insertIndex < 0)
            {
                target.Files.Add(file);
            }
            else
            {
                target.Files.Insert(insertIndex, file);
            }
        }
    }
}
