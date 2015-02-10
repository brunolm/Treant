using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treant.Domain;
using Treant.Core.Extenders;
using Treant.Core;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Treant.Services;
using System.Threading;
using Treant.Services.Authentication;

namespace Treant.ViewModels
{
    [Export]
    public class EditBoardViewModel : WindowViewModel
    {
        private BoardService boardService;

        private Board originalBoard = new Board();
        private Board currentBoard;
        public Board CurrentBoard
        {
            get { return currentBoard; }
            set
            {
                currentBoard = value;
                this.RaisePropertyChanged();

                if (value != originalBoard)
                    originalBoard = EntityService.Clone(value);
            }
        }

        public RelayCommand SaveCommand { get; private set; }

        [ImportingConstructor]
        public EditBoardViewModel(BoardService boardService)
        {
            this.boardService = boardService;

            CurrentBoard = new Board();

            SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }

        private void CancelCommandExecute(object obj)
        {
            CurrentBoard.Name = originalBoard.Name;
            (obj as Window).Close();
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
