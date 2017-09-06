using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWSiteClassLibrary
{
    public class Factory
    {
        public Factory()
        {
            // 
            // TODO: Add constructor logic here
            //
        }

        public static IPlayer CreatePlayerService()
        {
            return new Player();
        }

        public static IUser CreateUserService()
        {
            return new User();
        }

        public static IMap CreateMapService()
        {
            return new Map();
        }

        public static IScore CreateScoreService()
        {
            return new Score();
        }
    }
}
