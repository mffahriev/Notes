using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.NoteServices
{
    public class PathManagerNote : IPathManager<Note>
    {
        private readonly ApplicationContext _context;
        private readonly IPathManager<Catalog?> _catalogPathManager;

        public PathManagerNote(
            ApplicationContext context,
            IPathManager<Catalog?> catalogPathManager
        )
        {
            _context = context;
            _catalogPathManager = catalogPathManager;
        }

        public async Task<Note> Get(string fullPath, string userId, CancellationToken cancellationToken)
        {
            GetNoteNameCategoryPath(fullPath, out string noteName, out string baseCategoryPath);

            Catalog? catalog = await _catalogPathManager.Get(baseCategoryPath, userId, cancellationToken);

            Note note = await _context
                .Notes
                .AsNoTracking()
                .SingleAsync(
                    x => x.User.Id == userId
                    && x.Name == noteName,
                    cancellationToken
                );

            return note;
        }

        private void GetNoteNameCategoryPath(string fullPath, out string noteName, out string baseCategoryPath)
        {
            baseCategoryPath = Path.GetDirectoryName($"{fullPath}.txt") ?? throw new InvalidOperationException();
            noteName = Path.GetFileName($"{fullPath}.txt") ?? throw new InvalidOperationException();
        }
    }
}
