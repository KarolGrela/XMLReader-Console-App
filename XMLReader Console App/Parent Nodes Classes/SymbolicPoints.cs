using System;
using System.Collections.Generic;
using System.Text;

namespace XMLReader_Console_App
{
    /// <summary>
    /// List of Symbolic Points
    /// </summary>
    class SymbolicPoints
    {
        List<SymbolicPoint> symbolic_points;     

        public SymbolicPoints()
        {
            symbolic_points = new List<SymbolicPoint>();
        }
    }

    /// <summary>
    /// Symbolic point data consists of:
    ///     its own data saved in strings
    ///     instances of classes:
    ///         holdrules
    ///         resources
    ///     list segment links (list<seg>)
    /// </summary>
    class SymbolicPoint
    {
        #region Data [string]
        private string icon_type;
        private string name;
        private string id;
        private string x;
        private string y;
        private string h;
        private string level;
        private string opposite_h;
        private string drawx;
        private string drawy;
        private string decision_data;
        private string load;
        private string unload;
        private string hold;
        private string symboltype;
        private string occupancy_area;          // not used, don't know the datatype
        private string allowed_destination;     // not used, don't know the datatype

        public string Icon_type { get => icon_type; set => icon_type = value; }
        public string Name { get => name; set => name = value; }
        public string Id { get => id; set => id = value; }
        public string X { get => x; set => x = value; }
        public string Y { get => y; set => y = value; }
        public string H { get => h; set => h = value; }
        public string Level { get => level; set => level = value; }
        public string Opposite_h { get => opposite_h; set => opposite_h = value; }
        public string Drawx { get => drawx; set => drawx = value; }
        public string Drawy { get => drawy; set => drawy = value; }
        public string Decision_data { get => decision_data; set => decision_data = value; }
        public string Load { get => load; set => load = value; }
        public string Unload { get => unload; set => unload = value; }
        public string Hold { get => hold; set => hold = value; }
        public string Symboltype { get => symboltype; set => symboltype = value; }
        public string Occupancy_area { get => occupancy_area; set => occupancy_area = value; }
        #endregion

        #region Data [classes and lists]
        Holdrules holdrules;
        List<Seg> segment_links;
        Resources resources;
        #endregion

        #region Constructor
        public SymbolicPoint()
        {
            holdrules = new Holdrules();
            segment_links = new List<Seg>();
            resources = new Resources();
        }
        #endregion
    }

    /// <summary>
    /// Class storing string data
    /// </summary>
    class Holdrules
    {
        #region Data [string]
        private string btn_default;
        private string btn_alt1;
        private string mes;
        private string dest_default;
        private string dest_default_id;
        private string dest_alt1;
        private string dest_alt1_id;

        public string Btn_default { get => btn_default; set => btn_default = value; }
        public string Btn_alt1 { get => btn_alt1; set => btn_alt1 = value; }
        public string Mes { get => mes; set => mes = value; }
        public string Dest_default { get => dest_default; set => dest_default = value; }
        public string Dest_default_id { get => dest_default_id; set => dest_default_id = value; }
        public string Dest_alt1 { get => dest_alt1; set => dest_alt1 = value; }
        public string Dest_alt1_id { get => dest_alt1_id; set => dest_alt1_id = value; }
        #endregion
    }

    /// <summary>
    /// Class storing string data
    /// </summary>
    class Resources
    {
        #region Data [string]
        private string resource_type;
        private string resource_quantity_all;

        public string Resource_type { get => resource_type; set => resource_type = value; }
        public string Resource_quantity_all { get => resource_quantity_all; set => resource_quantity_all = value; }
        #endregion
    }

    /// <summary>
    /// Lacking knowledge of following datatypes:
    /// </summary>
    class OccupancyArea
    {

    }
    /// <summary>
    /// Lacking knowledge of following datatypes:
    /// </summary>
    class AllowedDestination
    {

    }

}
