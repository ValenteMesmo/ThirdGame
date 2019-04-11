﻿namespace Common
{
    public interface Inputs
    {
        bool IsPressingLeft { get; set; }
        bool WasPressingLeft { get; }

        bool IsPressingRight { get; set; }
        bool WasPressingRight { get; }

        bool IsPressingJump { get; set; }
        bool WasPressingJump { get; }

        void Update();
    }


}
