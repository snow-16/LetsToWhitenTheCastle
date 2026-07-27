using System;

[Flags]
public enum InputType
{
    Go = 1 << 0,
    Sprint = 1 << 1,
    Jump = 1 << 2,
}
