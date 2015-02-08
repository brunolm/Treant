namespace Treant.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Treant.Core;
    using Treant.DataProvider;
    using Treant.Domain;

    [Export]
    public class BoardService : AuthenticatedBasedService
    {
        public IEnumerable<Board> GetUserBoards(int? id = null)
        {
            var userID = id ?? CurrentUserID;

            using (var db = MefBootstrap.Resolve<ApplicationDbContext>())
            {
                return db.Boards
                    .Where(o => o.Owner.ID == userID && !o.Deleted)
                    .ToList();
            }
        }
    }
}
