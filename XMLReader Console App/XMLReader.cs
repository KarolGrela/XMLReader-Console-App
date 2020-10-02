using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XMLReader_Console_App.Parent_Nodes_Classes;

namespace XMLReader_Console_App
{
    class XMLReader
    {

        #region Boolean fields

        private bool b_file_info;
        private bool b_segment_data;
        private bool b_posinit_points;
        private bool b_symbolic_points;
        private bool b_symbolic_point_groups;
        private bool b_zones;

        /* setters/getters, not used
        public bool bIn_file_info { get => b_in_file_info; set => b_in_file_info = value; }
        public bool bSegment_data { get => b_segment_data; set => b_segment_data = value; }
        public bool bPosinit_points { get => b_posinit_points; set => b_posinit_points = value; }
        public bool bSymbolic_points { get => b_symbolic_points; set => b_symbolic_points = value; }
        public bool bSymbolic_point_groups { get => b_symbolic_point_groups; set => b_symbolic_point_groups = value; }
        public bool bZones { get => b_zones; set => b_zones = value; }
        */

        #endregion

        #region XML Files
        private XmlReader xmlFile;
        private string path;
        public string Path { get => path; set => path = value; }
        #endregion

        #region Data classes
        public FileInfo fileInfo;
        public PosinitPoints posinitPoints;
        public SegmentData segmentData;
        public SymbolicPointGroups symbolicPointGroups;
        public SymbolicPoints symbolicPoints;
        public Zones zones;
        #endregion

        #region Constructor
        public XMLReader(string path)
        {
            Path = path;                                // saving path to string
            xmlFile = XmlReader.Create(path);           // initialize XmlReader variable
            // initialization of parent nodes class instances
            fileInfo = new FileInfo();
            posinitPoints = new PosinitPoints();
            segmentData = new SegmentData();
            symbolicPointGroups = new SymbolicPointGroups();
            symbolicPoints = new SymbolicPoints();
            zones = new Zones();


            // initialization of boolean fields
            b_file_info = false;
            b_segment_data = false;
            b_posinit_points = false;
            b_symbolic_points = false;
            b_symbolic_point_groups = false;
            b_zones = false;
    }
        #endregion

        #region Methods
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="path">  </param>
        /// <returns> true if function finished succesfully, false if excepction occured </returns>
        public bool XMLReader_Workflow()
        {
            // Reading XML file
            while (xmlFile.Read()) {

                // setting/resetting boolean state bits
                SwitchBooleans(xmlFile.NodeType, xmlFile.Name);     

                // calling functions reading
                if(!(b_file_info || b_segment_data || b_posinit_points || b_symbolic_points || b_symbolic_point_groups || b_zones))
                {
                    // do nothing
                }
                else if(b_file_info)
                {
                    metFileInfo(xmlFile.NodeType, xmlFile.Name);
                }
                else if (b_segment_data)
                {
                    metSegmentData(xmlFile.NodeType, xmlFile.Name);
                }
                else if (b_posinit_points)
                {
                    metPosinit_point(xmlFile.NodeType, xmlFile.Name);
                }
                else if (b_symbolic_points)
                {
                    metSymbolic_points(xmlFile.NodeType, xmlFile.Name);
                }
                else if (b_symbolic_point_groups)
                {
                    metSymbolic_point_groups(xmlFile.NodeType, xmlFile.Name);
                }
                else if (b_zones)
                {
                    metZones(xmlFile.NodeType, xmlFile.Name);
                }               

            }

            Console.WriteLine(fileInfo.DisplayData());

            return true;
        }

        /// <summary>
        /// Switching logic of boolean fields
        /// </summary>
        /// <param name="type"> Type of currently read node </param>
        /// <param name="name"> Name of currently read node </param>
        private void SwitchBooleans(XmlNodeType type, string name)
        {
            /*
             * 
             * *****    SETTING IS DONE BEFORE RESETTING SINCE TRUE ON PREVIOUS STATE IS ONE OF CONDITIONS THAT HAVE TO BE MET FOR RESET TO WR   *****
             * 
             */
            #region Bit setting

            if (type == XmlNodeType.Element && name == "file_info")                                     b_file_info = true;                  // set if node is of type Element and has proper name node 
            if (type == XmlNodeType.Element && name == "segment_data" && b_file_info)                   b_segment_data = true;                  // set if node is of type Element and has proper name node and previous section is true
            if (type == XmlNodeType.Element && name == "posinit_points" && b_segment_data)              b_posinit_points = true;                // set if node is of type Element and has proper name node and previous section is true
            if (type == XmlNodeType.Element && name == "symbolic_points" && b_posinit_points)           b_symbolic_points = true;               // set if node is of type Element and has proper name node and previous section is true
            if (type == XmlNodeType.Element && name == "symbolic_point_groups" && b_symbolic_points)    b_symbolic_point_groups = true;         // set if node is of type Element and has proper name node and previous section is true
            if (type == XmlNodeType.Element && name == "zones" && b_symbolic_point_groups)              b_zones = true;                         // set if node is of type Element and has proper name node and previous section is true

            #endregion


            #region Bit resetting

            if (b_segment_data)                                                     b_file_info = false;                 // reset if previous section is true
            if (b_posinit_points)                                                   b_segment_data = false;                 // reset if previous section is true
            if (b_symbolic_points)                                                  b_posinit_points = false;               // reset if previous section is true
            if (b_symbolic_point_groups)                                            b_symbolic_points = false;              // reset if previous section is true
            if (b_zones)                                                            b_symbolic_point_groups = false;        // reset if previous section is true
            if (type == XmlNodeType.EndElement && name == "segment_file_v2")        b_zones = false;                        // reset if finishing reading the file

            #endregion

           // Console.WriteLine($"{type}, {name}");
            //Console.WriteLine($"{b_file_info}    {b_segment_data}    {b_posinit_points}      {b_symbolic_points}      {b_symbolic_point_groups}     {b_zones}");
           
        }

        #region Methods reading sections of files
        // reads file_info section
        // every cycle of this loop reads one line from XML
        private void metFileInfo(XmlNodeType type, string name)             
        {
            // saving data from file to variables
            if (type == XmlNodeType.Element && name == "version") fileInfo.Version = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "production_area") fileInfo.Production_area = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "machine_type") fileInfo.Machine_type = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "status") fileInfo.Status = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "timestamp") fileInfo.Timestamp = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "SW") fileInfo.SW1 = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "user") fileInfo.User = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "env_version") fileInfo.Environment_version = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "machine_type_id") fileInfo.Machine_type_id = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "custom_prop_1_name") fileInfo.Custom_prop_1_name = xmlFile.ReadElementContentAsString();
            else if (type == XmlNodeType.Element && name == "custom_prop_2_name") fileInfo.Custom_prop_2_name = xmlFile.ReadElementContentAsString();
        }

        // reads segment_data section
        // every cycle of this loop reads one segment
        private void metSegmentData(XmlNodeType type, string name)
        {
            Segment s1 = new Segment();
                
            ////// NIE DZIAŁA!!!!
                // loop going through whole segment
                while(true){
                    xmlFile.Read(); // reading next line
                    if (xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.ReadElementContentAsString() == "segment") break; // break loop if end of segment has been acheved

                    // if end of segment hasn't been yet achieved, save values to instance

                    //saving data of segment, unbounded by other classes/structures/lists
                    if (xmlFile.ReadElementContentAsString() == "id") s1.Id = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.ReadElementContentAsString() == "status") s1.Status = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.ReadElementContentAsString() == "level") s1.Level = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.ReadElementContentAsString() == "wait_area") s1.Wait_area = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.ReadElementContentAsString() == "pivot_start") s1.Pivot_start = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.ReadElementContentAsString() == "pivot_end") s1.Pivot_end = xmlFile.ReadElementContentAsString();
                }

            segmentData.AddSegment(s1);
        }

        private void metPosinit_point(XmlNodeType type, string name)
        {
            // do nothing
        }

        private void metSymbolic_points(XmlNodeType type, string name)
        {

        }

        private void metSymbolic_point_groups(XmlNodeType type, string name)
        {
            // do nothing
        }

        private void metZones(XmlNodeType type, string name)
        {
            // do nothing
        }

        #endregion

        #endregion


    }
}
