using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XMLReader_Console_App.Parent_Nodes_Classes;
using System.Data.SqlTypes;

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

        // string with read data
        public string dataString;

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

        #region Public Methods
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



            return true;
        }

        public string metCreateDataString()
        {
            dataString = "";
            dataString += fileInfo.DisplayData();
            dataString += segmentData.DisplayData();
            return dataString;
        }

        #endregion

        #region Private Methods

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

        // reads file_info section
        // every cycle of this loop reads one line from XML
        private void metFileInfo(XmlNodeType type, string name)             
        {
            // saving data from file to variables
            // if xmlFile is of type "Element" and of proper name, then:
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
        // every call of this method reads one segment
        private void metSegmentData(XmlNodeType type, string name)
        {
            Segment s1 = new Segment();     // variable storing data from currently read segment

            /*
             * Segment is devided to 5 data sets:
             * 1. segment data - 6 private strings
             * 2. pts - a list of instances of SP class
             * 3. start_links - list of strings
             * 4. end_links - list of strings
             * 5. seg_overlap - list of instances of seg class
             * In order for loop to function correctly boolean variables, determining type of currently read data, are needed:
             */
            bool segment_data = false;      // currently reading segment_data etc.
            bool pts = false;
            bool start_links = false;
            bool end_links = false;
            bool seg_overlap = false;

            

            // loop reading one segment
            while (xmlFile.Read())
            {
                // setting/resetting of boolean variables
                if (((pts || start_links || end_links || seg_overlap) == false) && xmlFile.Name == "id")  // set segment_data
                {
                    segment_data = true;
                }
                else if (segment_data && xmlFile.Name == "pts")                                              // reset segment data / set pts
                {
                    segment_data = false;
                    pts = true;
                }
                else if (pts && xmlFile.Name == "start_links")                                               // reset pts / set start_links
                {
                    pts = false;
                    start_links = true;
                }
                else if (start_links && xmlFile.Name == "end_links")                                         // reset start_links / set end_links
                {
                    start_links = false;
                    end_links = true;
                }
                else if (end_links && xmlFile.Name == "seg_overlap")                                         // reset end_links / set seg_overlap
                {
                    end_links = false;
                    seg_overlap = true;
                }
                else if (seg_overlap && xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.Name == "seg_overlap")     // reset seg_overlap
                {
                    seg_overlap = false;
                }

                //Console.WriteLine($"{segment_data} {pts} {start_links} {end_links} {seg_overlap} {xmlFile.NodeType } {xmlFile.Name}");


                // Saving data to the structure
                if (segment_data)
                {
                    // reading data
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "id") s1.Id = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "status") s1.Status = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "level") s1.Level = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "wait_area") s1.Wait_area = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "pivot_start") s1.Pivot_start = xmlFile.ReadElementContentAsString();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "pivot_end") s1.Pivot_end = xmlFile.ReadElementContentAsString();
                }
               
                // saving instances of class sp to list called pts
                if(pts)
                {
                    if(xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "sp")
                    {
                        Sp sp = new Sp();
                        while(xmlFile.Read())
                        {
                            // reading data
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "x") { sp.X = xmlFile.ReadElementContentAsString(); }
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "y") { sp.Y = xmlFile.ReadElementContentAsString(); }
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "s") { sp.S = xmlFile.ReadElementContentAsString(); }
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "t") { sp.T = xmlFile.ReadElementContentAsString(); }
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "actuator") { sp.Actuator = xmlFile.ReadElementContentAsString(); }
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "strict_actuator") { sp.Strict_actuator = xmlFile.ReadElementContentAsString(); }
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "p1") { sp.P1 = xmlFile.ReadElementContentAsString(); }
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "p2") { sp.P2 = xmlFile.ReadElementContentAsString(); }

                            // adding sp instance to the list and breaking the loop
                            if (xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.Name == "sp")
                            {
                                s1.pts.Add(sp);
                                break;
                            }
                        }
                    }
                }

                // add seg_ids to list start_links
                if(start_links)
                {
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "start_links")
                    {
                        while (xmlFile.Read())
                        {
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "seg_id")    s1.start_links.Add(xmlFile.ReadElementContentAsString()); // add element to list
                            if (xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.Name == "start_links")
                            {
                                break;
                            }
                        }
                    }
                }

                // add seg_ids to list end_links
                if (end_links)
                {
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "end_links")
                    {
                        while (xmlFile.Read())
                        {
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "seg_id")    s1.end_links.Add(xmlFile.ReadElementContentAsString()); // add element to list
                            if (xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.Name == "end_links")
                            {
                                break;
                            }
                        }
                    }
                }

                
                if (seg_overlap)
                {
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "seg")
                    {
                        Seg seg = new Seg();
                        while (xmlFile.Read())
                        {
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "machine_type_id")   seg.Machine_type_id = xmlFile.ReadElementContentAsString();
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "id")                seg.Id = xmlFile.ReadElementContentAsString();
                            if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name == "overlap_modes")     seg.Overlap_modes = xmlFile.ReadElementContentAsString();

                            if (xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.Name == "seg")
                            {
                                s1.seg_overlap.Add(seg);
                                break;
                            }
                        }
                    }
                }
                

                // adding segment to the list and breaking the loop up -> segment finished
                if (xmlFile.NodeType == XmlNodeType.EndElement && xmlFile.Name == "segment") 
                {
                    // Console.WriteLine("Break dem loops");
                    segmentData.AddSegment(s1); 
                    break;
                }
            }
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


    }
}
