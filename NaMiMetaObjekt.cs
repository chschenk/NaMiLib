using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaMiLib
{
    class NaMiMetaObjekt
    {
        public List<NaMiField> fields { get; set; }
        public List<object> relations { get; set; }
        public List<object> actions { get; set; }
        public string packageName { get; set; }
        public string simpleClassName { get; set; }
        public bool flist { get; set; }
        public int pageSize { get; set; }
        public string label { get; set; }
        public bool description { get; set; }
        public List<object> extraActions { get; set; }
        public bool groupable { get; set; }
        public string tabGroups { get; set; }
        public string groupingType { get; set; }
        public bool loadDefaultEntity { get; set; }
        public string mainDataTabLabel { get; set; }
        public bool withActionsOnMultipleRows { get; set; }
        public bool tpWithActions { get; set; }
        public string uniqueId { get; set; }
        public object tpFilteringPropertyName { get; set; }
        public List<object> associatedFilterableEntities { get; set; }
        public bool extraActionsInExtraToolbar { get; set; }
    }
}
