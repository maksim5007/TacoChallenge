﻿namespace TacoChallenge.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly TacoChallengeDbContext _context;

        public InitialHostDbBuilder(TacoChallengeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new DefaultEntriesCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
