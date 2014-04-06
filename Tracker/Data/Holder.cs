using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tracker.Data
{
    public static class Holder
    {
        public static string serverBaseName = "";
        public static double latestPositionsUpdate = -1;
        public static double allPositionsUpdate = -1;

        public static bool dataIsBeeingUpdated = false;
        public static string sectionsSelected = "";
        public static string teamsSelected = "";

        public static Teams teams;
        public static RaceSetup race;
        public static CourseSetup course;
        public static List<Contour> contours;



    }
}
