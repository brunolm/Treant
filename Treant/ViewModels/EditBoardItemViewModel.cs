namespace Treant.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using Treant.Core;
    using Treant.Core.Extenders;
    using Treant.Domain;
    using Treant.Services;
    using Treant.Services.Authentication;

    [Export]
    public class EditBoardItemViewModel : WindowViewModel
    {
        private BoardService boardService;

        [ImportingConstructor]
        public EditBoardItemViewModel(BoardService boardService)
        {
            this.boardService = boardService;

            CurrentTaskItem = new TaskItem();

            SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }


        private TaskItem originalTaskItem;
        private TaskItem currentTaskItem;

        [PropertyChanged.DoNotCheckEquality]
        public TaskItem CurrentTaskItem
        {
            get
            {
                return this.currentTaskItem;
            }
            set
            {
                this.currentTaskItem = value;
                this.originalTaskItem = EntityService.Clone(value);
            }
        }

        public RelayCommand SaveCommand { get; private set; }

        private void CancelCommandExecute(object obj)
        {
            var wnd = (obj as Window);

            this.currentTaskItem = this.originalTaskItem;

            wnd.DialogResult = false;
            wnd.Close();
        }

        private bool SaveCommandCanExecute(object arg)
        {
            var result = CurrentTaskItem.Validate();

            return result.Valid;// || !result.ValidationResults.Any(o => !o.MemberNames.Contains("OwnerBoard"));
        }

        private void SaveCommandExecute(object obj)
        {
            var window = obj as Window;

            var result = boardService.Save(CurrentTaskItem);

            if (result.Valid)
            {
                window.DialogResult = true;
                window.Close();
            }
            else
            {
                MessageBox.Show(String.Join(Environment.NewLine, result.ValidationResults.Select(o => o.ErrorMessage))
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
            }
        }
    }
}
