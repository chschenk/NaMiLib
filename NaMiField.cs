using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaMiLib
{
    public class NaMiField
    {
        public string name { get; set; }
        public string type { get; set; }
        public string inputType { get; set; }
        public bool visible { get; set; }
        public string enabled { get; set; }
        public string serviceUrl { get; set; }
        public bool searchable { get; set; }
        public bool sortable { get; set; }
        public bool listable { get; set; }
        public string labelId { get; set; }
        public string label { get; set; }
        public int order { get; set; }
        public string tabGroupAssociation { get; set; }
        public bool hidden { get; set; }
        public string columnCssClass { get; set; }
        public string tooltipText { get; set; }
        public string labelCssClass { get; set; }
        public bool mandatory { get; set; }
        public List<string> actions { get; set; }
        public string regex { get; set; }
        public string regexText { get; set; }
        public List<object> dependentOn { get; set; }
        public int? pageSize { get; set; }
        public bool multiSelect { get; set; }

        public List<NaMiObjekt> Auswahl { get; set; }

        public List<string> search {get; set;}
        public void UpdateAuswahl(Connector con)
        {
            if(Auswahl == null && (!serviceUrl.Equals("")) && visible && (dependentOn == null || dependentOn.Count == 0))
            {
                this.Auswahl = con.GetData<List<NaMiObjekt>>(Commands.Deploy + serviceUrl);
            }
        }
    }
}
