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
        public List<SymbolicPoint> symbolic_points_list;     

        public SymbolicPoints()
        {
            symbolic_points_list = new List<SymbolicPoint>();
        }
    }

    /// <summary>
    /// Symbolic point class has different structure, depending from type of symbolic point
    /// We've assumed all symbolic points are Hold
    /// If other types are to be used in the future, modifications will be required
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
        private string stop;
        private string symboltype;


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
        public string Stop { get => stop; set => stop = value; }
        public string Symboltype { get => symboltype; set => symboltype = value; }

        #endregion

        #region Unknown Datatypes

        private string occupancy_area;          // not used, don't know the datatype
        private string allowed_destination;     // not used, don't know the datatype

        public string Occupancy_area { get => occupancy_area; set => occupancy_area = value; }
        public string Allowed_destination { get => allowed_destination; set => allowed_destination = value; }

        #endregion

        #region Data [classes and lists]
        public List<SymP_Seg> segment_links;
        public Resources resources;
        #endregion

        #region Constructor
        public SymbolicPoint()
        {
            segment_links = new List<SymP_Seg>();
            resources = new Resources();
        }
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

    class SymP_Seg
    {
        private string machine_type_id;
        private string id;
        private string point_index;

        public string Machine_type_id { get => machine_type_id; set => machine_type_id = value; }
        public string Id { get => id; set => id = value; }
        public string Point_index { get => point_index; set => point_index = value; }
    }
}
