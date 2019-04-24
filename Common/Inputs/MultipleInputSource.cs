namespace Common
{
    public class MultipleInputSource : Inputs
    {
        private readonly Inputs[] inputs;

        public Direction Direction { get; set; }
        //public bool WasPressingLeft { get; private set; }

        //public bool IsPressingRight { get; set; }
        //public bool WasPressingRight { get; private set; }

        //public bool IsPressingJump { get; set; }
        //public bool WasPressingJump { get; private set; }

        //public bool IsPressingDown { get; set; }
        //public bool WasPressingDown { get; private set; }

        //public bool IsPressingUp { get; set; }
        //public bool WasPressingUp { get; private set; }

        public MultipleInputSource(params Inputs[] inputs)
        {
            this.inputs = inputs;
        }

        public void Update()
        {
            Direction = Direction.None;

            foreach (var item in inputs)
            {
                item.Update();

                if (item.Direction != Direction.None)
                    Direction = item.Direction;
            }
        }
    }


}
