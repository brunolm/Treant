namespace Treant.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Treant.Core;
    using Treant.DataProvider;
    using Treant.Domain;
    using Treant.Services.Authentication;

    [Export]
    public class BoardService : AuthenticatedBasedService
    {
        public IEnumerable<Board> GetUserBoards(int? id = null)
        {
            var userID = id ?? CurrentUserID;

            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                return db.Boards
                    .Where(o => o.OwnerID == userID && !o.Deleted)
                    .OrderBy(o => o.Name)
                    .ToList();
            }
        }

        public OperationResult Save(Board board)
        {
            if (board.OwnerID == default(int))
                board.OwnerID = CurrentUserID;

            return base.Save(board);
        }

        public bool Remove(Board board)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            if (board.OwnerID != CurrentUserID)
                throw new UnauthorizedAccessException("Current user does not own this entity");

            return base.Remove(board);
        }


        

        // TODO: Remove
        public void CreateDummies()
        {
            var user = (Thread.CurrentPrincipal.Identity as CustomIdentity).CurrentUser;

            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                db.Users.Attach(user);

                if (db.Boards.Count() == 0)
                {
                    var boards = new List<Board>
                    {
                        new Board { Name = "C#", Owner = user },
                        new Board { Name = "WPF", Owner = user },
                        new Board { Name = "People Skills", Owner = user },
                        new Board { Name = "Some other board", Owner = user },
                        new Board { Name = "I can haz many boards", Owner = user },
                        new Board { Name = "Look I'm a potato", Owner = user },
                    };

                    db.Boards.AddRange(boards);

                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<TaskItem> GetTaskItems(Board board)
        {
            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                var taskItems = db.Set<Board>()
                    .Include(o => o.TaskItems)
                    .Single(o => o.ID == board.ID)
                    .TaskItems.Where(o => !o.TaskItemParentID.HasValue).ToList();

                return taskItems;
            }
        }
    }
}
