namespace Common
{
    public class NetworkInputs : Inputs
    {
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

        public void AfterUpdate()
        {
            WasPressingLeft = IsPressingLeft;
            WasPressingRight = IsPressingRight;
            WasPressingJump = IsPressingJump;
            WasPressingDown = IsPressingDown;
            WasPressingUp = IsPressingUp;
        }

        public void Update()
        {
        }
    }


}
