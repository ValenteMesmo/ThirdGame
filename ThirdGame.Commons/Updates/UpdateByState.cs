using Common;
using System;
using System.Collections.Generic;

namespace ThirdGame
{
    public class UpdateByState : IHandleUpdates
    {
        private readonly Dictionary<int, IHandleUpdates> Options = new Dictionary<int, IHandleUpdates>();
        private readonly IHaveState gameOjbect;

        public UpdateByState(IHaveState gameOjbect) =>
            this.gameOjbect = gameOjbect;

        public void Update()
        {
            Options[gameOjbect.State].Update();
        }

        public void Add(int state, IHandleUpdates updateHandler)
        {
            if (Options.ContainsKey(state))
                throw new Exception($"{nameof(UpdateByState)} already have an update handler for state {state}");

            Options[state] = updateHandler;
        }
    }
}
