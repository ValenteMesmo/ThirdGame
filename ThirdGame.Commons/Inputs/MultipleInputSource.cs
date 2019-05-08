﻿namespace Common
{
    public class MultipleInputSource : Inputs
    {
        private readonly Inputs[] inputs;

        public DpadDirection Direction { get; set; }
        public bool Jump { get; set; }

        public MultipleInputSource(params Inputs[] inputs)
        {
            this.inputs = inputs;
        }

        public void Update()
        {
            Direction = DpadDirection.None;
            Jump = false;

            foreach (var item in inputs)
            {
                item.Update();

                if (item.Direction != DpadDirection.None)
                    Direction = item.Direction;

                if (!Jump)
                    Jump = item.Jump;
            }
        }
    }


}