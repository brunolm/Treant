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
    public class EditBoardViewModel : WindowViewModel
    {
        private BoardService boardService;

        [ImportingConstructor]
        public EditBoardViewModel(BoardService boardService)
        {
            this.boardService = boardService;

            CurrentBoard = new Board();

            SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }

        private Board originalBoard;
        private Board currentBoard;

        [PropertyChanged.DoNotCheckEquality]
        public Board CurrentBoard
        {
            get
            {
                return this.currentBoard;
            }
            set
            {
                this.currentBoard = value;
                this.originalBoard = EntityService.Clone(value);
            }
        }

        public RelayCommand SaveCommand { get; private set; }

        private void CancelCommandExecute(object obj)
        {
            var wnd = (obj as Window);

            this.currentBoard = originalBoard;

            wnd.DialogResult = false;
            wnd.Close();
        }

        private bool SaveCommandCanExecute(object arg)
        {
            var result = CurrentBoard.Validate();

            return result.Valid || !result.ValidationResults.Any(o => !o.MemberNames.Contains("Owner"));
        }

        private void SaveCommandExecute(object obj)
        {
            var window = obj as Window;

            var result = boardService.Save(CurrentBoard);

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
