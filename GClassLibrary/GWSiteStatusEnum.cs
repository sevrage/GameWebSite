using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWSiteClassLibrary
{
    public enum GWSiteStatusEnum
    {
        OK                  = 1,
        NOT_OK              = 0,
        INVALID_LOGIN       = -1,
        INVALID_KEY         = -2,
        INVALID_USER_ID     = -3,
        INVALID_PLAYER_ID   = -4,
        INVALID_MAP_ID      = -5,
        INVALID_GAME_ID     = -6,
        ERROR               = -7,
        INVALID_ARGUMENT    = -8
    }
}
