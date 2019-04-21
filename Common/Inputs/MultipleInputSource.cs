﻿namespace Common
{
    public class MultipleInputSource : Inputs
    {
        private readonly Inputs[] inputs;

        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public bool IsPressingDown { get; set; }
        public bool WasPressingDown { get; private set; }

        public bool IsPressingUp { get; set; }
        public bool WasPressingUp { get; private set; }

        public MultipleInputSource(params Inputs[] inputs)
        {
            this.inputs = inputs;
        }

        public void Update()
        {
            IsPressingLeft = false;
            IsPressingRight = false;
            IsPressingJump = false;
            IsPressingDown = false;
            IsPressingUp = false;

            foreach (var item in inputs)
            {
                item.Update();

                if (item.IsPressingLeft)
                    IsPressingLeft = true;
                if (item.IsPressingRight)
                    IsPressingRight = true;
                if (item.IsPressingJump)
                    IsPressingJump = true;
                if (item.IsPressingDown)
                    IsPressingDown = true;
                if (item.IsPressingUp)
                    IsPressingUp = true;
            }
        }
    }


}
